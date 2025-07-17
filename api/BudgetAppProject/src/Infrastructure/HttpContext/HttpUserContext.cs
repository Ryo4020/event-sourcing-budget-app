namespace BudgetAppProject.Infrastructure.HttpContext;

using BudgetAppProject.Application.Common;

public class HttpUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        return user?.FindFirst("sub")?.Value ?? throw new InvalidOperationException("User ID not found in the context.");
    }
}