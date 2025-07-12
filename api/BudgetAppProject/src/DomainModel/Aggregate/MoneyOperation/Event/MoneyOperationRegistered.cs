namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

public class MoneyOperationRegistered : MoneyOperationEvent
{
    public MoneyOperation EventTarget { get; init; }

    public MoneyOperationRegistered(MoneyOperation moneyOperation) : base()
    {
        EventTarget = moneyOperation;
    }
    
    public MoneyOperationRegistered(string eventId, DateTimeOffset eventAt, MoneyOperation moneyOperation) : base(eventId, eventAt)
    {
        EventTarget = moneyOperation;
    }
}