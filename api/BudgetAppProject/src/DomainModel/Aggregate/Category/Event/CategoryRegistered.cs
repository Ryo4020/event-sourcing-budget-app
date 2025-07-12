namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

public class CategoryRegistered : CategoryEvent
{
    public Category EventTarget { get; init; }

    public CategoryRegistered(Category category) : base()
    {
        EventTarget = category;
    }
    
    public CategoryRegistered(string eventId, DateTimeOffset eventAt, Category category) : base(eventId, eventAt)
    {
        EventTarget = category;
    }
}