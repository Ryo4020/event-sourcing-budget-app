namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryEdited : DomainEvent
{
    public string EventTargetid { get; init; }
    
    public string NewName { get; init; }

    public CategoryEdited(string categoryId, string newName) : base()
    {
        EventTargetid = categoryId;
        NewName = newName;
    }
}