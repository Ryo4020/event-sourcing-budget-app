namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

public class CategoryRenamed : CategoryEvent
{
    public string EventTargetId { get; init; }

    public string NewName { get; init; }

    public CategoryRenamed(string categoryId, string newName) : base()
    {
        EventTargetId = categoryId;
        NewName = newName;
    }
    
    public CategoryRenamed(string eventId, DateTimeOffset eventAt, string categoryId, string newName) : base(eventId, eventAt)
    {
        EventTargetId = categoryId;
        NewName = newName;
    }
}