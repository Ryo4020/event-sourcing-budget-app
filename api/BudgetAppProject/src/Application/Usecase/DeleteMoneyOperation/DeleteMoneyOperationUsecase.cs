namespace BudgetAppProject.Application.Usecase.DeleteMoneyOperation;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;

public class DeleteMoneyOperationUsecase : IDeleteMoneyOperationUsecase
{
    private readonly IEventPublisher<MoneyOperationDeleted> _eventPublisher;
    private readonly IUserContext _userContext;

    public DeleteMoneyOperationUsecase(IEventPublisher<MoneyOperationDeleted> eventPublisher, IUserContext userContext)
    {
        _eventPublisher = eventPublisher;
        _userContext = userContext;
    }

    public async Task<DeleteMoneyOperationResponse> HandleAsync(DeleteMoneyOperationRequest request)
    {
        var userId = _userContext.GetUserId();

        var deletedEvent = new MoneyOperationDeleted(request.Id);
        await _eventPublisher.Publish(deletedEvent);

        return new DeleteMoneyOperationResponse {};
    }
}