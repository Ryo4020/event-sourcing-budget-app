namespace BudgetAppProject.Extensions;

public static class EnvironmentVariablesExtension
{
    public static void SetDatabaseVariables()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        string stage = GetStageByEnvironment(environmentName);

        Environment.SetEnvironmentVariable("MONEY_OPERATION_EVENT_TABLE_NAME", GetServiceResourceName("MoneyOperationEventTable", stage));
        Environment.SetEnvironmentVariable("CATEGORY_STATE_TABLE_NAME", GetServiceResourceName("CategoryStateTable", stage));
    }

    public static void SetAuthVariables()
    {
        string awsRegion = GetAwsRegion();

        string? userPoolId = Environment.GetEnvironmentVariable("COGNITO_USER_POOL_ID");
        if (string.IsNullOrEmpty(userPoolId))
        {
            throw new InvalidOperationException("Environment variable 'COGNITO_USER_POOL_ID' is not set.");
        }

        Environment.SetEnvironmentVariable("COGNITO_AUTHORITY", $"https://cognito-idp.{awsRegion}.amazonaws.com/{userPoolId}");
    }

    private static string GetStageByEnvironment(string environmentName)
    {
        return environmentName switch
        {
            "Development" => "DEV",
            "Staging" => "STG",
            "Production" => "PR",
            _ => throw new ArgumentException($"Unknown environment: {environmentName}")
        };
    }
    
    private static string GetAwsRegion()
    {
        return Environment.GetEnvironmentVariable("AWS_REGION") ?? "ap-northeast-1";
    }

    private static string GetServiceResourceName(string name, string stage) =>
        $"EventSourcingBudgetApp{name}-{stage}";
}