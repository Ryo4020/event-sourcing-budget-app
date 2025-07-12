namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

public class MoneyOperationDeleted : MoneyOperationEvent
{
    public string EventTargetId { get; init; }

    public MoneyOperationDeleted(string moneyOperationId) : base()
    {
        EventTargetId = moneyOperationId;
    }
    
    public MoneyOperationDeleted(string eventId, DateTimeOffset eventAt, string moneyOperationId) : base(eventId, eventAt)
    {
        EventTargetId = moneyOperationId;
    }
}