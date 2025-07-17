namespace BudgetAppProject.Application.Usecase.GetMoneyOperations;

using System.ComponentModel.DataAnnotations;

public readonly record struct GetMoneyOperationsRequest
{
    public string? SearchTitleText { get; init; }

    [DataType(DataType.DateTime, ErrorMessage = "OperationStartAt must be a valid date and time.")]
    public DateTimeOffset? OperationStartAt { get; init; }

    [DataType(DataType.DateTime, ErrorMessage = "OperationEndAt must be a valid date and time.")]
    public DateTimeOffset? OperationEndAt { get; init; }

    [AllowedValues(1, 2, ErrorMessage = "Type must be either 1 (Income) or 2 (Expense).")]
    public int? Type { get; init; }

    public List<string>? CategoryIds { get; init; }
};