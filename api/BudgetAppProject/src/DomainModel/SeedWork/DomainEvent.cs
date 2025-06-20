namespace BudgetAppProject.DomainModel.SeedWork;

using BudgetAppProject.DomainModel.Utils;

public abstract class DomainEvent
{
    public string EventId { get; }

    public DateTimeOffset EventAt { get; }

    protected DomainEvent(DateTimeOffset eventAt)
    {
        EventId = UuidGenerator.NewUuid();
        EventAt = eventAt;
    }
}