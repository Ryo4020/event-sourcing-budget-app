namespace BudgetAppProject.Application.Usecase.GetIncomeAndExpenses;

using System.ComponentModel.DataAnnotations;

public readonly record struct GetIncomeAndExpensesRequest
{
    [Required(ErrorMessage = "UserId is required.", AllowEmptyStrings = false)]
    public required string UserId { get; init; }
};