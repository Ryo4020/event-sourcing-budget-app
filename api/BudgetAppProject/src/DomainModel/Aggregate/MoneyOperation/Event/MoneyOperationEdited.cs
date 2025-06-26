namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

using BudgetAppProject.DomainModel.SeedWork;

public class MoneyOperationEdited : DomainEvent
{
    public MoneyOperation EventTarget { get; init; }

    public MoneyOperationEdited (MoneyOperation moneyOperation) : base()
    {
        EventTarget = moneyOperation;
    }
}