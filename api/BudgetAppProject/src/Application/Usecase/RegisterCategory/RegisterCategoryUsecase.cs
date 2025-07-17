namespace BudgetAppProject.Application.Usecase.RegisterCategory;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainModel.Aggregate.Category;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class RegisterCategoryUsecase : IRegisterCategoryUsecase
{
    private readonly IEventPublisher<CategoryRegistered> _eventPublisher;

    private readonly IUserContext _userContext;

    public RegisterCategoryUsecase(IEventPublisher<CategoryRegistered> eventPublisher, IUserContext userContext)
    {
        _eventPublisher = eventPublisher;
        _userContext = userContext;
    }

    public async Task<RegisterCategoryResponse> HandleAsync(RegisterCategoryRequest request)
    {
        var userId = _userContext.GetUserId();

        var category = EditableCategory.Create(
            request.Name,
            userId
        );

        var registeredEvent = new CategoryRegistered(category);
        await _eventPublisher.Publish(registeredEvent);

        return new RegisterCategoryResponse {};
    }
}