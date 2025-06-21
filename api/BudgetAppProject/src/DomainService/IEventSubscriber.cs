namespace BudgetAppProject.DomainService;

using BudgetAppProject.DomainModel.SeedWork;

public interface IEventSubscriber<TEvent> where TEvent : DomainEvent
{
    Task Handle(TEvent domainEvent);
}