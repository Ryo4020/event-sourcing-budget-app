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
        var incomeAndExpenses = await _moneyOperationDataAccess.GetIncomeAndExpensesByUserIdAsync(userId);

        return new GetIncomeAndExpensesResponse
        {
            TotalIncome = incomeAndExpenses.TotalIncome,
            TotalExpenses = incomeAndExpenses.TotalExpenses,
            TotalBalance = incomeAndExpenses.TotalBalance
        };
    }
}