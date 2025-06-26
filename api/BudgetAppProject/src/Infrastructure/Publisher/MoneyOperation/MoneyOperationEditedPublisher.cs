namespace BudgetAppProject.Infrastructure.Publisher.Category;

using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;

public class MoneyOperationEditedPublisher : IEventPublisher<MoneyOperationEdited>
{
    private readonly IEnumerable<IEventSubscriber<MoneyOperationEdited>> _subscribers = [];

    public MoneyOperationEditedPublisher(IEnumerable<IEventSubscriber<MoneyOperationEdited>> subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Publish(MoneyOperationEdited domainEvent)
    {
        // invoke handlers of all subscribers
        foreach (var subscriber in _subscribers)
        {
            await subscriber.Handle(domainEvent);
        }
    }
}