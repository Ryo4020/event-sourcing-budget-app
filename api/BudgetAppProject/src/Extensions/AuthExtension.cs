namespace BudgetAppProject.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class AuthExtension
{
    public static IServiceCollection AddCognitoAuthentication(this IServiceCollection services, bool isAuthenticationEnabled)
    {
        if (!isAuthenticationEnabled)
        {
            // If authentication is not enabled, set up authentication that always succeeds (Development mode only)
            throw new NotImplementedException("Always succeed authentication is not implemented yet.");
        }

        var cognitoAuthority = Environment.GetEnvironmentVariable("COGNITO_AUTHORITY");
        if (string.IsNullOrEmpty(cognitoAuthority))
        {
            throw new InvalidOperationException("Environment variable 'COGNITO_AUTHORITY' is not set.");
        }

        var cognitoClinetId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID");
        if (string.IsNullOrEmpty(cognitoClinetId))
        {
            throw new InvalidOperationException("Environment variable 'COGNITO_CLIENT_ID' is not set.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = cognitoAuthority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = cognitoAuthority,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false
                };

                // Audience検証をclient_idクレームで行うようにカスタマイズ
                // options.Events = new JwtBearerEvents
                // {
                //     OnTokenValidated = context =>
                //     {
                //         var claims = context.Principal?.Claims;
                //         if (claims == null)
                //         {
                //             context.Fail("No claims found in the token.");
                //             return Task.CompletedTask;
                //         }

                //         Console.WriteLine($"Claims: {string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}"))}");

                //         var clientIdClaim = claims.FirstOrDefault(c => c.Type == "client_id");

                //         if (clientIdClaim == null || clientIdClaim.Value != cognitoClinetId)
                //         {
                //             context.Fail("Invalid client ID.");
                //         }
                //         return Task.CompletedTask;
                //     }
                // };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authorizationHeader = context.Request.Headers.Authorization.ToString();
                        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                        {
                            // "Bearer "プレフィックスを取り除いてトークンを取得
                            context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();
                        }
                        Console.WriteLine($"Token: {context.Token}");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    public static bool IsAuthenticationEnabled()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var userPoolId = Environment.GetEnvironmentVariable("COGNITO_USER_POOL_ID");

        return !(string.IsNullOrEmpty(userPoolId) && environmentName == "Development");
    }
}