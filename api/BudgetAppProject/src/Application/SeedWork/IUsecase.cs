namespace BudgetAppProject.Application.SeedWork;

public interface IUsecase<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}