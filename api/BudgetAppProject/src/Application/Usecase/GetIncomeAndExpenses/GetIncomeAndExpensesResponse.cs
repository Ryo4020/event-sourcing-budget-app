namespace BudgetAppProject.Application.Usecase.GetIncomeAndExpenses;

public readonly record struct GetIncomeAndExpensesResponse
{
    public required uint TotalIncome { get; init; }

    public required uint TotalExpenses { get; init; }

    public required int TotalBalance { get; init; }
};