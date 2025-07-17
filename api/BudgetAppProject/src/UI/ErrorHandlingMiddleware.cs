namespace BudgetAppProject.UI;

using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            var problem = new ProblemDetails
            {
                Status = 400,
                Title = "Bad Request",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (KeyNotFoundException ex)
        {
            var problem = new ProblemDetails
            {
                Status = 404,
                Title = "Not Found",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (ConditionalCheckFailedException ex)
        {
            var problem = new ProblemDetails
            {
                Status = 409,
                Title = "Conflict Data Error",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = 409;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (InvalidDataException ex)
        {
            var problem = new ProblemDetails
            {
                Status = 422,
                Title = "Unprocessable Entity",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = 422;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}