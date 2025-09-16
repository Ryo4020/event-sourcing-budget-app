namespace BudgetAppProject.Application.Usecase.EditMoneyOperation;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;

public class EditMoneyOperationUsecase : IEditMoneyOperationUsecase
{
    private readonly IEventPublisher<MoneyOperationEdited> _eventPublisher;
    private readonly IUserContext _userContext;

    public EditMoneyOperationUsecase(IEventPublisher<MoneyOperationEdited> eventPublisher, IUserContext userContext)
    {
        _eventPublisher = eventPublisher;
        _userContext = userContext;
    }

    public async Task<EditMoneyOperationResponse> HandleAsync(EditMoneyOperationRequest request)
    {
        var userId = _userContext.GetUserId();

        var moneyOperation = new MoneyOperation(
            request.MoneyOperation.Id,
            request.MoneyOperation.Title,
            request.MoneyOperation.Description,
            request.MoneyOperation.Price,
            request.MoneyOperation.OperationAt,
            (MoneyOperationType)request.MoneyOperation.Type,
            request.MoneyOperation.UserId,
            request.MoneyOperation.CategoryId
        );

        var editedEvent = new MoneyOperationEdited(moneyOperation);
        await _eventPublisher.Publish(editedEvent);

        return new EditMoneyOperationResponse {};
    }
}