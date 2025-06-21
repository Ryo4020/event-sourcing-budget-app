namespace BudgetAppProject.DomainService.DataAccess;

using System.Collections.Immutable;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;

public interface IMoneyOperationDataAccess
{
    MoneyOperation FindById(string id, string userId);

    ImmutableArray<MoneyOperation> FindAll(string userId);
}