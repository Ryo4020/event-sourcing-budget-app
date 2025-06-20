namespace BudgetAppProject.DomainModel.Utils;

public class UuidGenerator
{
    public static string NewUuid()
    {
        return Guid.NewGuid().ToString();
    }
}