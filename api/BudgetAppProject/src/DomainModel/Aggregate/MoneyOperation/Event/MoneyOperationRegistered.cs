namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class MoneyOperationRegistered : DomainEvent
{
    public required MoneyOperation EventTarget { get; init; }

    public MoneyOperationRegistered (DateTimeOffset eventAt, MoneyOperation moneyOperation) : base(eventAt)
    {
        EventTarget = moneyOperation;
    }
}