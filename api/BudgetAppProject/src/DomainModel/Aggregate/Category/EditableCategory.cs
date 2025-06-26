namespace BudgetAppProject.DomainModel.Aggregate.Category;

using BudgetAppProject.DomainModel.Utils;

public class EditableCategory : Category
{
    public string UserId { get; init; }

    public EditableCategory(string id, string name, string userId) : base(id, name, false)
    {
        UserId = userId;
    }

    public static EditableCategory Create(
        string name,
        string userId
    )
    {
        var id = UuidGenerator.NewUuid();
        return new EditableCategory(
            id,
            name,
            userId
        );
    }

    public void Rename(string name)
    {
        SetName(name);
    }
}