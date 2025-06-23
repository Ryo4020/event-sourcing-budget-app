namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryRenamed : DomainEvent
{
    public string EventTargetid { get; init; }
    
    public string NewName { get; init; }

    public CategoryRenamed(string categoryId, string newName) : base()
    {
        EventTargetid = categoryId;
        NewName = newName;
    }
}