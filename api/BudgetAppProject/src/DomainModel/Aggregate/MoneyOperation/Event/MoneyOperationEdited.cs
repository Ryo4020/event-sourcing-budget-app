namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class MoneyOperationEdited : DomainEvent
{
    public required MoneyOperation EventTarget { get; init; }

    public MoneyOperationEdited (DateTimeOffset eventAt, MoneyOperation moneyOperation) : base(eventAt)
    {
        EventTarget = moneyOperation;
    }
}