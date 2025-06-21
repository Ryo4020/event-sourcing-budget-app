namespace BudgetAppProject.DomainModel.Aggregate.Category;

using BudgetAppProject.DomainModel.Utils;

public class Category
{
    public string Id { get; init; }

    public string Name { get; private set; }

    public bool IsDefault { get; init; }

    public string UserId { get; init; }

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

    public void Rename(string name)
    {
        if (IsDefault)
        {
            throw new InvalidOperationException("Cannot rename a default category.");
        }

        Name = name;
    }
}