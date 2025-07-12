namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public abstract class CategoryEvent : DomainEvent
{
    protected CategoryEvent() : base()
    {}
    
    protected CategoryEvent(string eventId, DateTimeOffset eventAt) : base(eventId, eventAt)
    {}
}