namespace BudgetAppProject.Infrastructure.Publisher.Category;

using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class CategoryRegisteredPublisher : IEventPublisher<CategoryRegistered>
{
    private readonly IEnumerable<IEventSubscriber<CategoryRegistered>> _subscribers = [];

    public CategoryRegisteredPublisher(IEnumerable<IEventSubscriber<CategoryRegistered>> subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Publish(CategoryRegistered domainEvent)
    {
        // invoke handlers of all subscribers
        foreach (var subscriber in _subscribers)
        {
            await subscriber.Handle(domainEvent);
        }
    }
}