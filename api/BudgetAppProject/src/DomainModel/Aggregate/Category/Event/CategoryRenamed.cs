namespace BudgetAppProject.DomainModel.Aggregate.Category.Event;

public class CategoryRenamed : CategoryEvent
{
    public string EventTargetId { get; init; }
    
    public string NewName { get; init; }

    public CategoryRenamed(string categoryId, string newName) : base()
    {
        EventTargetId = categoryId;
        NewName = newName;
    }
}