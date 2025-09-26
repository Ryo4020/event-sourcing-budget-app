namespace BudgetAppProject.Application.Usecase.GetIncomeAndExpenses;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainService.DataAccess;

public class GetIncomeAndExpensesUsecase : IGetIncomeAndExpensesUsecase
{
    private readonly IMoneyOperationDataAccess _moneyOperationDataAccess;
    private readonly IUserContext _userContext;

    public GetIncomeAndExpensesUsecase(IMoneyOperationDataAccess moneyOperationDataAccess, IUserContext userContext)
    {
        _moneyOperationDataAccess = moneyOperationDataAccess;
        _userContext = userContext;
    }

    public async Task<GetIncomeAndExpensesResponse> HandleAsync(GetIncomeAndExpensesRequest request)
    {
        var userId = _userContext.GetUserId();
        var moneyOperations = await _moneyOperationDataAccess.FindAll(userId);

        uint totalIncome = 0;
        uint totalExpenses = 0;

        foreach (var operation in moneyOperations)
        {
            if (operation.Type == DomainModel.Aggregate.MoneyOperation.MoneyOperationType.Income)
            {
                totalIncome += operation.Price;
            }
            else if (operation.Type == DomainModel.Aggregate.MoneyOperation.MoneyOperationType.Expense)
            {
                totalExpenses += operation.Price;
            }
        }

        int totalBalance = (int)totalIncome - (int)totalExpenses;

        return new GetIncomeAndExpensesResponse
        {
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            TotalBalance = totalBalance
        };
    }
}