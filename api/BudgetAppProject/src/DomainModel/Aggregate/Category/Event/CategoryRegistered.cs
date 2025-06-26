namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class CategoryRegistered : DomainEvent
{
    public Category EventTarget { get; init; }

    public CategoryRegistered (Category category) : base()
    {
        EventTarget = category;
    }
}