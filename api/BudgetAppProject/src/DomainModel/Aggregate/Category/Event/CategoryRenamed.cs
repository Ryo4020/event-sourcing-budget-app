namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryRenamed : DomainEvent
{
    public string EventTargetId { get; init; }
    
    public string NewName { get; init; }

    public CategoryRenamed(string categoryId, string newName) : base()
    {
        EventTargetId = categoryId;
        NewName = newName;
    }
}