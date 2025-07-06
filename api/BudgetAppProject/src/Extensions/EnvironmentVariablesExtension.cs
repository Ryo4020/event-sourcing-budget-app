namespace BudgetAppProject.Extensions;

public static class EnvironmentVariablesExtension
{
    public static void SetDatabaseVariables()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        string stage = GetStageByEnrionment(environmentName);

        Environment.SetEnvironmentVariable("MONEY_OPERATION_EVENT_TABLE_NAME", GetServiceResourceName("MoneyOperationEventTable", stage));
        Environment.SetEnvironmentVariable("CATEGORY_STATE_TABLE_NAME", GetServiceResourceName("CategoryStateTable", stage));
    }

    private static string GetStageByEnrionment(string environmentName)
    {
        return environmentName switch
        {
            "Development" => "DEV",
            "Staging" => "STG",
            "Production" => "PR",
            _ => throw new ArgumentException($"Unknown environment: {environmentName}")
        };
    }

    private static string GetServiceResourceName(string name, string stage) => 
        $"EventSourcingBudgetApp{name}-{stage}";
}