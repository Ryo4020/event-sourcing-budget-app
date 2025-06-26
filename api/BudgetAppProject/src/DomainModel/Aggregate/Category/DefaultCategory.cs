namespace BudgetAppProject.DomainModel.Aggregate.Category;

using BudgetAppProject.DomainModel.Utils;

public class DefaultCategory : Category
{
    public static string NameReplaceDeleted => "その他";

    public DefaultCategory(string id, string name) : base(id, name, true)
    {}

    public static DefaultCategory Create(
        string name
    )
    {
        var id = UuidGenerator.NewUuid();
        return new DefaultCategory(
            id,
            name
        );
    }
}