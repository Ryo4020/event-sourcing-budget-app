namespace BudgetAppProject.Application.Usecase.DeleteCategory;

using System.ComponentModel.DataAnnotations;

public readonly record struct DeleteCategoryRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public required string Id { get; init; }
};