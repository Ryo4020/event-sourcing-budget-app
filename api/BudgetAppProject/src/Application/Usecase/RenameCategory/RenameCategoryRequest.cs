namespace BudgetAppProject.Application.Usecase.RenameCategory;

using System.ComponentModel.DataAnnotations;

public readonly record struct RenameCategoryRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public required string Id { get; init; }

    [Required(ErrorMessage = "NewName is required.", AllowEmptyStrings = false)]
    public required string NewName { get; init; }
};