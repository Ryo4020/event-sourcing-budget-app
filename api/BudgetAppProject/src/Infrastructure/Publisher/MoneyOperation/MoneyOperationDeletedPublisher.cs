namespace BudgetAppProject.Infrastructure.Publisher.Category;

using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;

public class MoneyOperationDeletedPublisher : IEventPublisher<MoneyOperationDeleted>
{
    private readonly IEnumerable<IEventSubscriber<MoneyOperationDeleted>> _subscribers = [];

    public MoneyOperationDeletedPublisher(IEnumerable<IEventSubscriber<MoneyOperationDeleted>> subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Publish(MoneyOperationDeleted domainEvent)
    {
        // invoke handlers of all subscribers
        foreach (var subscriber in _subscribers)
        {
            await subscriber.Handle(domainEvent);
        }
    }
}