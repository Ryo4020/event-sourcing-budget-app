namespace BudgetAppProject.Application.Usecase.GetCategories;

using System.ComponentModel.DataAnnotations;

public readonly record struct GetCategoriesRequest
{
    [Required(ErrorMessage = "UserId is required.", AllowEmptyStrings = false)]
    public required string UserId { get; init; }
};