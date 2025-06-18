namespace BudgetAppProject.Application.Usecase.EditMoneyOperation;

using System.ComponentModel.DataAnnotations;
using BudgetAppProject.Application.Dto;

public readonly record struct EditMoneyOperationRequest
{
    [Required(ErrorMessage = "MoneyOperation is required.")]
    public required MoneyOperationDto MoneyOperation { get; init; }
};