namespace BudgetAppProject.Extensions;

using BudgetAppProject.Application.Usecase.RegisterCategory;

public static class DIExtension
{
    public static IServiceCollection AddUsecases(this IServiceCollection services)
    {
        services.AddTransient<IRegisterCategoryUsecase, RegisterCategoryUsecase>();

        return services;
    }
}