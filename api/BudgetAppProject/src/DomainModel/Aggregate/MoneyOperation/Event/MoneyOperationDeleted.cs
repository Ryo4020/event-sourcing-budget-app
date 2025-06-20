namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class MoneyOperationDeleted : DomainEvent
{
    public required string EventTargetId { get; init; }

    public MoneyOperationDeleted (DateTimeOffset eventAt, string moneyOperationId) : base(eventAt)
    {
        EventTargetId = moneyOperationId;
    }
}