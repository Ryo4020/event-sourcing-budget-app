namespace BudgetAppProject.DomainModel.Aggregate.Category;

using BudgetAppProject.DomainModel.Utils;

public class Category
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public bool IsDefault { get; private set; }

    public string UserId { get; private set; }

    public Category(
        string id,
        string name,
        bool isDefault,
        string userId
    )
    {
        Id = id;
        Name = name;
        IsDefault = isDefault;
        UserId = userId;
    }

    public static Category Create(
        string name,
        string userId
    )
    {
        var id = UuidGenerator.NewUuid();
        return new Category(
            id,
            name,
            false,
            userId
        );
    }

    public void Rename(
        string name
    )
    {
        Name = name;
    }
}