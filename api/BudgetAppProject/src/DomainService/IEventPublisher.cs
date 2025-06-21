namespace BudgetAppProject.DomainService;

using BudgetAppProject.DomainModel.SeedWork;

public interface IEventPublisher<TEvent> where TEvent : DomainEvent
{
    Task Publish(TEvent domainEvent);
}