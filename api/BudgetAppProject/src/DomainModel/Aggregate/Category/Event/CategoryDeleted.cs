namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

public class CategoryDeleted : CategoryEvent
{
    public string EventTargetId { get; init; }

    public CategoryDeleted (string categoryId) : base()
    {
        EventTargetId = categoryId;
    }
}