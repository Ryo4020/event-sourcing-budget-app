namespace BudgetAppProject.Application.Usecase.GetMoneyOperations;

using BudgetAppProject.Application.Dto;

public readonly record struct GetMoneyOperationsResponse
{
    public required List<MoneyOperationDto> MoneyOperations { get; init; }
};