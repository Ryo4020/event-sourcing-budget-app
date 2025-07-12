namespace BudgetAppProject.Infrastructure.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;
using BudgetAppProject.DomainService.DataAccess;
using BudgetAppProject.Infrastructure.DataAccess.AWS;

public class MoneyOperationDataAccess :
    IMoneyOperationDataAccess,
    IEventSubscriber<MoneyOperationRegistered>,
    IEventSubscriber<MoneyOperationEdited>,
    IEventSubscriber<MoneyOperationDeleted>
{
    private readonly MoneyOperationEventTableDao _moneyOperationEventTableDao;

    public MoneyOperationDataAccess(MoneyOperationEventTableDao moneyOperationEventTableDao)
    {
        _moneyOperationEventTableDao = moneyOperationEventTableDao;
    }

    public async Task<MoneyOperation> FindById(string id)
    {
        var events = await _moneyOperationEventTableDao.GetEventsByIdAsync(id);

        var moneyOperations = ReplayEvents(events);
        if (moneyOperations.Count == 0)
        {
            throw new KeyNotFoundException($"MoneyOperation with id '{id}' not found.");
        } else if (moneyOperations.Count > 1)
        {
            throw new InvalidOperationException($"Multiple MoneyOperations found for id '{id}'.");
        }

        return moneyOperations[0];
    }

    public async Task<ImmutableArray<MoneyOperation>> FindAll(string userId)
    {
        var events = await _moneyOperationEventTableDao.GetEventsByUserIdAsync(userId);
        if (events.IsEmpty)
        {
            return [];
        }

        return ReplayEvents(events).ToImmutableArray();
    }

    public async Task<ImmutableArray<MoneyOperation>> FindAllByCategoryId(string categoryId, string? userId)
    {
        var events = await _moneyOperationEventTableDao.GetEventsByCategoryIdAsync(categoryId, userId);
        if (events.IsEmpty)
        {
            return [];
        }

        return ReplayEvents(events).ToImmutableArray();
    }

    public async Task Handle(MoneyOperationRegistered domainEvent)
    {
        await _moneyOperationEventTableDao.AddRegisteredEventAsync(domainEvent);
    }

    public async Task Handle(MoneyOperationEdited domainEvent)
    {
        await _moneyOperationEventTableDao.AddEditedEventAsync(domainEvent);
    }

    public async Task Handle(MoneyOperationDeleted domainEvent)
    {
        var moneyOperation = await FindById(domainEvent.EventTargetId);
        if (moneyOperation == null)
        {
            throw new KeyNotFoundException($"MoneyOperation with id '{domainEvent.EventTargetId}' not found for deletion.");
        }

        await _moneyOperationEventTableDao.AddDeletedEventAsync(domainEvent, moneyOperation.UserId, moneyOperation.CategoryId);
    }

    private static List<MoneyOperation> ReplayEvents(ImmutableArray<MoneyOperationEvent> events)
    {
        var orderedEvents = events.OrderBy(e => e.EventAt);

        var idToEntitiesMap = new Dictionary<string, MoneyOperation>();
        foreach (var eventItem in events)
        {
            switch (eventItem)
            {
                case MoneyOperationRegistered registeredEvent:
                    idToEntitiesMap[registeredEvent.EventTarget.Id] = registeredEvent.EventTarget;
                    break;
                case MoneyOperationEdited editedEvent:
                    if (idToEntitiesMap.ContainsKey(editedEvent.EventTarget.Id))
                    {
                        idToEntitiesMap[editedEvent.EventTarget.Id] = editedEvent.EventTarget;
                    }
                    break;
                case MoneyOperationDeleted deletedEvent:
                    idToEntitiesMap.Remove(deletedEvent.EventTargetId);
                    break;

                default:
                    throw new InvalidOperationException($"Unknown event type: {eventItem.GetType()}");
            }
        }

        return [.. idToEntitiesMap.Values];
    }
}