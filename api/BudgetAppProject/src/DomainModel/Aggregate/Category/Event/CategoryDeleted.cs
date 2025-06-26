namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryDeleted : DomainEvent
{
    public string EventTargetId { get; init; }

    public CategoryDeleted (string categoryId) : base()
    {
        EventTargetId = categoryId;
    }
}