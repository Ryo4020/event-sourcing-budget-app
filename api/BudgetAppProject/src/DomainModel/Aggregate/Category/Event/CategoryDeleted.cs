namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryDeleted : DomainEvent
{
    public required string EventTargetId { get; init; }

    public CategoryDeleted (DateTimeOffset eventAt, string categoryId) : base(eventAt)
    {
        EventTargetId = categoryId;
    }
}