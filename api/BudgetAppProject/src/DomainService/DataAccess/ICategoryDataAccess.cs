namespace BudgetAppProject.DomainService.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.Category;

public interface ICategoryDataAccess
{
    Task<Category> FindById(string id, string userId);

    Task<Category> FindByName(string name, string? userId);

    Task<ImmutableArray<Category>> FindAll(string userId);
}