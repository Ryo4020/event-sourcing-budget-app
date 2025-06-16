namespace BudgetAppProject.Application.Usecase.GetCategories;

using BudgetAppProject.Application.Dto;

public readonly record struct GetCategoriesResponse
{
    public required List<CategoryDto> Categories { get; init; }
};