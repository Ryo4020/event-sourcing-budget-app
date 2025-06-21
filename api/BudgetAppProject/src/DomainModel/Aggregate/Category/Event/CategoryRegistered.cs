namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryRegistered : DomainEvent
{
    public required Category EventTarget { get; init; }

    public CategoryRegistered (DateTimeOffset eventAt, Category category) : base(eventAt)
    {
        EventTarget = category;
    }
}