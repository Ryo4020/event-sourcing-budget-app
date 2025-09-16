namespace BudgetAppProject.Application.Usecase.GetMoneyOperations;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainService.DataAccess;

public class GetMoneyOperationsUsecase : IGetMoneyOperationsUsecase
{
    private readonly IMoneyOperationDataAccess _moneyOperationDataAccess;
    private readonly IUserContext _userContext;

    public GetMoneyOperationsUsecase(IMoneyOperationDataAccess moneyOperationDataAccess, IUserContext userContext)
    {
        _moneyOperationDataAccess = moneyOperationDataAccess;
        _userContext = userContext;
    }

    public async Task<GetMoneyOperationsResponse> HandleAsync(GetMoneyOperationsRequest request)
    {
        var userId = _userContext.GetUserId();
        var moneyOperations = await _moneyOperationDataAccess.GetByUserIdAsync(userId);

        return new GetMoneyOperationsResponse { MoneyOperations = moneyOperations };
    }
}