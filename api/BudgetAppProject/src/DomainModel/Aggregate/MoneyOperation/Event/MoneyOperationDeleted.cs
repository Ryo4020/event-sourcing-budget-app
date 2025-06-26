namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class MoneyOperationDeleted : DomainEvent
{
    public string EventTargetId { get; init; }

    public MoneyOperationDeleted (string moneyOperationId) : base()
    {
        EventTargetId = moneyOperationId;
    }
}