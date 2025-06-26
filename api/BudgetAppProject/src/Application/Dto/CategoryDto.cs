namespace BudgetAppProject.Application.Dto;

public readonly record struct CategoryDto
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required bool IsDefault { get; init; }

    public string? UserId { get; init; }
};