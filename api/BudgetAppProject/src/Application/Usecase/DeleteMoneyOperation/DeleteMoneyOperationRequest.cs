namespace BudgetAppProject.Application.Usecase.DeleteMoneyOperation;

using System.ComponentModel.DataAnnotations;

public readonly record struct DeleteMoneyOperationRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public required string Id { get; init; }
};