namespace BudgetAppProject.Application.Usecase.RegisterMoneyOperation;

using System.ComponentModel.DataAnnotations;

public readonly record struct RegisterMoneyOperationRequest
{
    [Required(ErrorMessage = "Title is required.", AllowEmptyStrings = false)]
    public required string Title { get; init; }

    public string? Description { get; init; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive integer.")]
    public required uint Price { get; init; }

    [Required(ErrorMessage = "OperationAt is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "OperationAt must be a valid date and time.")]
    public required DateTimeOffset OperationAt { get; init; }

    [Required(ErrorMessage = "Type is required.")]
    [Range(1, 2, ErrorMessage = "Type must be either 1 (Income) or 2 (Expense).")]
    public required uint Type { get; init; }

    [Required(ErrorMessage = "CategoryId is required.", AllowEmptyStrings = false)]
    public required string CategoryId { get; init; }
};