namespace BudgetAppProject.Application.Usecase.RegisterMoneyOperation;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;

public class RegisterMoneyOperationUsecase : IRegisterMoneyOperationUsecase
{
    private readonly IEventPublisher<MoneyOperationRegistered> _eventPublisher;
    private readonly IUserContext _userContext;

    public RegisterMoneyOperationUsecase(IEventPublisher<MoneyOperationRegistered> eventPublisher, IUserContext userContext)
    {
        _eventPublisher = eventPublisher;
        _userContext = userContext;
    }

    public async Task<RegisterMoneyOperationResponse> HandleAsync(RegisterMoneyOperationRequest request)
    {
        var userId = _userContext.GetUserId();

        var moneyOperation = MoneyOperation.Create(
            request.Title,
            request.Description,
            request.Price,
            request.OperationAt,
            (MoneyOperationType)request.Type,
            userId,
            request.CategoryId
        );

        var registeredEvent = new MoneyOperationRegistered(moneyOperation);
        await _eventPublisher.Publish(registeredEvent);

        return new RegisterMoneyOperationResponse {};
    }
}