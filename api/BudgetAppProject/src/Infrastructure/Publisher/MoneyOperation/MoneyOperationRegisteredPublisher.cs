namespace BudgetAppProject.Infrastructure.Publisher.Category;

using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;

public class MoneyOperationRegisteredPublisher : IEventPublisher<MoneyOperationRegistered>
{
    private readonly IEnumerable<IEventSubscriber<MoneyOperationRegistered>> _subscribers = [];

    public MoneyOperationRegisteredPublisher(IEnumerable<IEventSubscriber<MoneyOperationRegistered>> subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Publish(MoneyOperationRegistered domainEvent)
    {
        // invoke handlers of all subscribers
        foreach (var subscriber in _subscribers)
        {
            await subscriber.Handle(domainEvent);
        }
    }
}