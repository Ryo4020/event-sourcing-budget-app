namespace BudgetAppProject.Application.Usecase.GetCategories;

using BudgetAppProject.Application.Common;
using BudgetAppProject.DomainService.DataAccess;

public class GetCategoriesUsecase : IGetCategoriesUsecase
{
    private readonly ICategoryDataAccess _categoryDataAccess;
    private readonly IUserContext _userContext;

    public GetCategoriesUsecase(ICategoryDataAccess categoryDataAccess, IUserContext userContext)
    {
        _categoryDataAccess = categoryDataAccess;
        _userContext = userContext;
    }

    public async Task<GetCategoriesResponse> HandleAsync(GetCategoriesRequest request)
    {
        var userId = _userContext.GetUserId();
        var categories = await _categoryDataAccess.GetByUserIdAsync(userId);

        return new GetCategoriesResponse { Categories = categories };
    }
}