namespace BudgetAppProject.Application.Usecase.DeleteCategory;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class DeleteCategoryUsecase : IDeleteCategoryUsecase
{
    private readonly IEventPublisher<CategoryDeleted> _eventPublisher;
    private readonly IUserContext _userContext;

    public DeleteCategoryUsecase(IEventPublisher<CategoryDeleted> eventPublisher, IUserContext userContext)
    {
        _eventPublisher = eventPublisher;
        _userContext = userContext;
    }

    public async Task<DeleteCategoryResponse> HandleAsync(DeleteCategoryRequest request)
    {
        var userId = _userContext.GetUserId();

        var deletedEvent = new CategoryDeleted(request.Id);
        await _eventPublisher.Publish(deletedEvent);

        return new DeleteCategoryResponse {};
    }
}