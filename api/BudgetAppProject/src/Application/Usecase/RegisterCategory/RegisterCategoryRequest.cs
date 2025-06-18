namespace BudgetAppProject.Application.Usecase.RegisterCategory;

using System.ComponentModel.DataAnnotations;

public readonly record struct RegisterCategoryRequest
{
    [Required(ErrorMessage = "Name is required.", AllowEmptyStrings = false)]
    public required string Name { get; init; }

    [Required(ErrorMessage = "UserId is required.", AllowEmptyStrings = false)]
    public required string UserId { get; init; }
};