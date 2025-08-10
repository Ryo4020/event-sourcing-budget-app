using BudgetAppProject.Extensions;
using BudgetAppProject.UI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BudgetAppProject API", Version = "v1" });

    // JWT Bearer authentication configuration
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var isAuthenticationEnabled = AuthExtension.IsAuthenticationEnabled();

EnvironmentVariablesExtension.SetDatabaseVariables();
if (isAuthenticationEnabled)
{
    EnvironmentVariablesExtension.SetAuthVariables();
}

builder.Services.AddCognitoAuthentication(isAuthenticationEnabled);

builder.Services.AddUsecases();
builder.Services.AddDataAccess();
builder.Services.AddAwsContexts();
builder.Services.AddHttpContext();
builder.Services.AddPublishers();
builder.Services.AddSubscribers();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();