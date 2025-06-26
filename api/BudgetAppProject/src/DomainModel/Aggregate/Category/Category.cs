namespace BudgetAppProject.DomainModel.Aggregate.Category;

public abstract class Category
{
    public string Id { get; init; }

    public string Name { get; private set; }

    public bool IsDefault { get; init; }

    public Category(
        string id,
        string name,
        bool isDefault
    )
    {
        if (!isDefault && name == DefaultCategory.NameReplaceDeleted)
        {
            throw new ArgumentException("Cannot create a category with the default name.");
        }

        Id = id;
        Name = name;
        IsDefault = isDefault;
    }

    protected void SetName(string name)
    {
        if (name == DefaultCategory.NameReplaceDeleted)
        {
            throw new ArgumentException("Cannot rename a category to the default name.");
        }

        Name = name;
    }
}