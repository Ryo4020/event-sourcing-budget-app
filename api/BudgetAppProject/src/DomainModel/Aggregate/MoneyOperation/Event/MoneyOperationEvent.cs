namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public abstract class MoneyOperationEvent : DomainEvent
{
    protected MoneyOperationEvent() : base()
    {}
    
    protected MoneyOperationEvent(string eventId, DateTimeOffset eventAt) : base(eventId, eventAt)
    {}
}