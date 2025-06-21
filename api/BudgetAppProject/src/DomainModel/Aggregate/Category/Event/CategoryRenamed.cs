namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryEdited : DomainEvent
{
    public required string EventTargetid { get; init; }
    
    public required string NewName { get; init; }

    public CategoryEdited(DateTimeOffset eventAt, string categoryId, string newName) : base(eventAt)
    {
        EventTargetid = categoryId;
        NewName = newName;
    }
}