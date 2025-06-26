namespace BudgetAppProject.Infrastructure.Publisher.Category;

using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class CategoryRenamedPublisher : IEventPublisher<CategoryRenamed>
{
    private readonly IEnumerable<IEventSubscriber<CategoryRenamed>> _subscribers = [];

    public CategoryRenamedPublisher(IEnumerable<IEventSubscriber<CategoryRenamed>> subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Publish(CategoryRenamed domainEvent)
    {
        // invoke handlers of all subscribers
        foreach (var subscriber in _subscribers)
        {
            await subscriber.Handle(domainEvent);
        }
    }
}