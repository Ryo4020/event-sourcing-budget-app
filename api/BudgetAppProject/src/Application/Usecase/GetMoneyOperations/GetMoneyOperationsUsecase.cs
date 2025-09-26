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
        var moneyOperations = await _moneyOperationDataAccess.FindAll(userId);

        var moneyOperationDtos = moneyOperations.Select(m => new BudgetAppProject.Application.Dto.MoneyOperationDto
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            Price = m.Price,
            OperationAt = m.OperationAt,
            Type = (uint)m.Type,
            CategoryId = m.CategoryId
        }).ToList();

        return new GetMoneyOperationsResponse { MoneyOperations = moneyOperationDtos };
    }
}