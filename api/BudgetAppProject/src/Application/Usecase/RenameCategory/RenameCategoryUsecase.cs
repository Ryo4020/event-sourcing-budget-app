namespace BudgetAppProject.Application.Usecase.RenameCategory;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class RenameCategoryUsecase : IRenameCategoryUsecase
{
    private readonly IEventPublisher<CategoryRenamed> _eventPublisher;
    private readonly IUserContext _userContext;

    public RenameCategoryUsecase(IEventPublisher<CategoryRenamed> eventPublisher, IUserContext userContext)
    {
        _eventPublisher = eventPublisher;
        _userContext = userContext;
    }

    public async Task<RenameCategoryResponse> HandleAsync(RenameCategoryRequest request)
    {
        var userId = _userContext.GetUserId();

        var renamedEvent = new CategoryRenamed(request.Id, request.NewName);
        await _eventPublisher.Publish(renamedEvent);

        return new RenameCategoryResponse {};
    }
}