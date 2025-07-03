namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;

public class MoneyOperationEdited : MoneyOperationEvent
{
    public MoneyOperation EventTarget { get; init; }

    public MoneyOperationEdited (MoneyOperation moneyOperation) : base()
    {
        EventTarget = moneyOperation;
    }
}