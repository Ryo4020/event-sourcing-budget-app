namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

public class CategoryDeleted : CategoryEvent
{
    public string EventTargetId { get; init; }

    public CategoryDeleted(string categoryId) : base()
    {
        EventTargetId = categoryId;
    }
    
    public CategoryDeleted(string eventId, DateTimeOffset eventAt, string categoryId) : base(eventId, eventAt)
    {
        EventTargetId = categoryId;
    }
}