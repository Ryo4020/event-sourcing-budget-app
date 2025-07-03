namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

public class CategoryRegistered : CategoryEvent
{
    public Category EventTarget { get; init; }

    public CategoryRegistered (Category category) : base()
    {
        EventTarget = category;
    }
}