namespace BudgetAppProject.DomainService.Policy;

using BudgetAppProject.DomainModel.Aggregate.Category;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService.DataAccess;

public class CategoryReplaceIfDeletedPolicy : IEventSubscriber<CategoryDeleted>
{
    private readonly IMoneyOperationDataAccess _moneyOperationDataAccess;
    private readonly ICategoryDataAccess _categoryDataAccess;
    private readonly IEventPublisher<MoneyOperationEdited> _eventPublisher;

    public CategoryReplaceIfDeletedPolicy(
        IMoneyOperationDataAccess moneyOperationDataAccess,
        ICategoryDataAccess categoryDataAccess,
        IEventPublisher<MoneyOperationEdited> eventPublisher
    )
    {
        _moneyOperationDataAccess = moneyOperationDataAccess;
        _categoryDataAccess = categoryDataAccess;
        _eventPublisher = eventPublisher;
    }

    public async Task Handle(CategoryDeleted domainEvent)
    {
        var category = await _categoryDataAccess.FindByName(DefaultCategory.NameReplaceDeleted, null);
        if (category == null) throw new Exception("Default category for replacement if deleted not found");

        var moneyOperations = await _moneyOperationDataAccess.FindAllByCategoryId(domainEvent.EventTargetId);
        foreach (var moneyOperation in moneyOperations)
        {
            moneyOperation.Update(
                moneyOperation.Title,
                moneyOperation.Description,
                moneyOperation.Price,
                moneyOperation.OperationAt,
                moneyOperation.Type,
                category.Id
            );

            MoneyOperationEdited moneyOperationEdited = new MoneyOperationEdited(
                moneyOperation
            );

            await _eventPublisher.Publish(moneyOperationEdited);
        }
    }
}