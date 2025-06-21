namespace BudgetAppProject.DomainService.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.Category;

public interface ICategoryDataAccess
{
    Category FindById(string id, string userId);

    Category FindByName(string name, string userId);

    ImmutableArray<Category> FindAll(string userId);
}