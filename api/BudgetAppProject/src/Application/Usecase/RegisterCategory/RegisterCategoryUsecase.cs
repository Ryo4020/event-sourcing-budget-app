namespace BudgetAppProject.Application.Usecase.RegisterCategory;

using BudgetAppProject.DomainModel.Aggregate.Category;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainService;

public class RegisterCategoryUsecase : IRegisterCategoryUsecase
{
    private readonly IEventPublisher<CategoryRegistered> _eventPublisher;

    public RegisterCategoryUsecase(IEventPublisher<CategoryRegistered> eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task<RegisterCategoryResponse> HandleAsync(RegisterCategoryRequest request)
    {
        var category = EditableCategory.Create(
            request.Name,
            request.UserId
        );

        var registeredEvent = new CategoryRegistered(category);
        await _eventPublisher.Publish(registeredEvent);

        return new RegisterCategoryResponse {};
    }
}