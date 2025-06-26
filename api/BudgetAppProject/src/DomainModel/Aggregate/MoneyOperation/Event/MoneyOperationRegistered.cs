namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class MoneyOperationRegistered : DomainEvent
{
    public MoneyOperation EventTarget { get; init; }

    public MoneyOperationRegistered (MoneyOperation moneyOperation) : base()
    {
        EventTarget = moneyOperation;
    }
}