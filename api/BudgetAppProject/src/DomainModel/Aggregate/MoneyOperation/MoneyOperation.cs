namespace BudgetAppProject.DomainModel.Aggregate.MoneyOperation;

using BudgetAppProject.DomainModel.Utils;

public class MoneyOperation
{
    public string Id { get; init; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public uint Price { get; private set; }

    public DateTimeOffset OperationAt { get; private set; }

    public MoneyOperationType Type { get; private set; }

    public string UserId { get; init; }

    public string CategoryId { get; private set; }

    public MoneyOperation(
        string id,
        string title,
        string? description,
        uint price,
        DateTimeOffset operationAt,
        MoneyOperationType type,
        string userId,
        string categoryId
    )
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        OperationAt = operationAt;
        Type = type;
        UserId = userId;
        CategoryId = categoryId;
    }

    public static MoneyOperation Create(
        string title,
        string? description,
        uint price,
        DateTimeOffset operationAt,
        MoneyOperationType type,
        string userId,
        string categoryId
    )
    {
        var id = UuidGenerator.NewUuid();
        return new MoneyOperation(
            id,
            title,
            description,
            price,
            operationAt,
            type,
            userId,
            categoryId
        );
    }

    public void Update(
        string title,
        string? description,
        uint price,
        DateTimeOffset operationAt,
        MoneyOperationType type,
        string categoryId
    )
    {
        Title = title;
        Description = description;
        Price = price;
        OperationAt = operationAt;
        Type = type;
        CategoryId = categoryId;
    }
}