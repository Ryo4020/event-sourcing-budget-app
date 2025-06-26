namespace BudgetAppProject.DomainService.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;

public interface IMoneyOperationDataAccess
{
    Task<MoneyOperation> FindById(string id, string userId);

    Task<ImmutableArray<MoneyOperation>> FindAll(string userId);

    Task<ImmutableArray<MoneyOperation>> FindAllByCategoryId(string categoryId);
}