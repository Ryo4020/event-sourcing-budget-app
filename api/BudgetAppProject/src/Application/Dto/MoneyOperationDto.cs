namespace BudgetAppProject.Application.Dto;

public readonly record struct MoneyOperationDto
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required uint Price { get; init; }

    public required DateTimeOffset OperationAt { get; init; }

    public required uint Type { get; init; }

    public required string UserId { get; init; }

    public required string CategoryId { get; init; }
};