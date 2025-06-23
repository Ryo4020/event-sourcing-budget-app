namespace BudgetAppProject.Infrastructure.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.Category;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;
using BudgetAppProject.DomainService.DataAccess;

public class CategoryDataAccess :
    ICategoryDataAccess,
    IEventSubscriber<CategoryRegistered>,
    IEventSubscriber<CategoryRenamed>,
    IEventSubscriber<CategoryDeleted>
{
    public async Task<Category> FindById(string id, string userId)
    {
        Category category = new EditableCategory(id, "Sample Category", userId);
        return await Task.FromResult(category);
    }

    public async Task<Category> FindByName(string name, string userId)
    {
        Category category = new EditableCategory("sample-id", name, userId);
        return await Task.FromResult(category);
    }

    public async Task<ImmutableArray<Category>> FindAll(string userId)
    {
        ImmutableArray<Category> categories =
        [
            new EditableCategory("1", "Food", userId),
            new EditableCategory("2", "Transport", userId),
            new EditableCategory("3", "Entertainment", userId)
        ];

        return await Task.FromResult(categories);
    }

    public async Task Handle(CategoryRegistered domainEvent)
    {
        Console.WriteLine($"Category Registered: {domainEvent.EventTarget.Name}");
        await Task.CompletedTask;
    }

    public async Task Handle(CategoryRenamed domainEvent)
    {
        Console.WriteLine($"Category Renamed: {domainEvent.EventTargetId} to {domainEvent.NewName}");
        await Task.CompletedTask;
    }

    public async Task Handle(CategoryDeleted domainEvent)
    {
        Console.WriteLine($"Category Deleted: {domainEvent.EventTargetId}");
        await Task.CompletedTask;
    }
}