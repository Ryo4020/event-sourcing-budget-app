namespace BudgetAppProject.Infrastructure.Publisher.Category;

using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class CategoryDeletedPublisher : IEventPublisher<CategoryDeleted>
{
    private readonly IEnumerable<IEventSubscriber<CategoryDeleted>> _subscribers = [];

    public CategoryDeletedPublisher(IEnumerable<IEventSubscriber<CategoryDeleted>> subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Publish(CategoryDeleted domainEvent)
    {
        // invoke handlers of all subscribers
        foreach (var subscriber in _subscribers)
        {
            await subscriber.Handle(domainEvent);
        }
    }
}