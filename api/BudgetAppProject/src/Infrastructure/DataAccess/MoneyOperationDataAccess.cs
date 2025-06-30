namespace BudgetAppProject.Infrastructure.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainModel.Utils;
using BudgetAppProject.DomainService;
using BudgetAppProject.DomainService.DataAccess;

public class MoneyOperationDataAccess :
    IMoneyOperationDataAccess,
    IEventSubscriber<MoneyOperationRegistered>,
    IEventSubscriber<MoneyOperationEdited>,
    IEventSubscriber<MoneyOperationDeleted>
{
    public async Task<MoneyOperation> FindById(string id, string userId)
    {
        MoneyOperation moneyOperation = new MoneyOperation(id, "Sample MoneyOperation", null, 1, DatetimeHelper.Now(), MoneyOperationType.Income, userId, "1");
        return await Task.FromResult(moneyOperation);
    }

    public async Task<ImmutableArray<MoneyOperation>> FindAll(string userId)
    {
        ImmutableArray<MoneyOperation> moneyOperations =
        [
            new MoneyOperation("1", "Sample MoneyOperation", null, 1, DatetimeHelper.Now(), MoneyOperationType.Income, userId, "1")
        ];

        return await Task.FromResult(moneyOperations);
    }

    public async Task<ImmutableArray<MoneyOperation>> FindAllByCategoryId(string categoryId)
    {
        ImmutableArray<MoneyOperation> moneyOperations =
        [
            new MoneyOperation("1", "Sample MoneyOperation", null, 1, DatetimeHelper.Now(), MoneyOperationType.Income, "userId", categoryId)
        ];

        return await Task.FromResult(moneyOperations);
    }

    public async Task Handle(MoneyOperationRegistered domainEvent)
    {
        Console.WriteLine($"MoneyOperation Registered': {domainEvent.EventTarget.Title}");
        await Task.CompletedTask;
    }

    public async Task Handle(MoneyOperationEdited domainEvent)
    {
        Console.WriteLine($"MoneyOperation Edited': {domainEvent.EventTarget.Title}");
        await Task.CompletedTask;
    }

    public async Task Handle(MoneyOperationDeleted domainEvent)
    {
        Console.WriteLine($"MoneyOperation Deleted': {domainEvent.EventTargetId}");
        await Task.CompletedTask;
    }
}