namespace BudgetAppProject.Infrastructure.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.Category;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;
using BudgetAppProject.DomainService.DataAccess;
using BudgetAppProject.Infrastructure.DataAccess.AWS;

public class CategoryDataAccess :
    ICategoryDataAccess,
    IEventSubscriber<CategoryRegistered>,
    IEventSubscriber<CategoryRenamed>,
    IEventSubscriber<CategoryDeleted>
{
    private readonly CategoryStateTableDao _categoryStateTableDao;

    public CategoryDataAccess(CategoryStateTableDao categoryStateTableDao)
    {
        _categoryStateTableDao = categoryStateTableDao;
    }

    public async Task<Category> FindById(string id)
    {
        return await _categoryStateTableDao.GetByIdAsync(id);
    }

    public async Task<Category> FindByName(string name, string? userId)
    {
        var isDefault = string.IsNullOrEmpty(userId);
        var category = await _categoryStateTableDao.GetByNameAsync(name, isDefault, userId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with name '{name}' not found.");
        }

        return category;
    }

    public async Task<ImmutableArray<Category>> FindAll(string userId)
    {
        return await _categoryStateTableDao.GetAllByUserIdAsync(userId);
    }

    public async Task Handle(CategoryRegistered domainEvent)
    {
        string? categoryUserId = domainEvent.EventTarget is EditableCategory editableCategory ? editableCategory.UserId : null;

        var duplicate = await _categoryStateTableDao.GetByNameAsync(
            domainEvent.EventTarget.Name,
            domainEvent.EventTarget is DefaultCategory,
            categoryUserId
        );
        if (duplicate != null)
        {
            throw new ArgumentException($"Category with name '{domainEvent.EventTarget.Name}' already exists.");
        }

        await _categoryStateTableDao.AddStateAsync(domainEvent.EventTarget);
    }

    public async Task Handle(CategoryRenamed domainEvent)
    {
        var existingCategory = await _categoryStateTableDao.GetByIdAsync(domainEvent.EventTargetId);

        var duplicate = await _categoryStateTableDao.GetByNameAsync(
            domainEvent.NewName,
            existingCategory.IsDefault,
            existingCategory is EditableCategory editableCategory ? editableCategory.UserId : null
        );
        if (duplicate != null)
        {
            throw new ArgumentException($"Category with name '{domainEvent.NewName}' already exists.");
        }

        await _categoryStateTableDao.RenameAsync(domainEvent.EventTargetId, domainEvent.NewName);
    }

    public async Task Handle(CategoryDeleted domainEvent)
    {
        await _categoryStateTableDao.DeleteAsync(domainEvent.EventTargetId);
    }
}