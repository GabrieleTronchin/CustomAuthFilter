using Microsoft.AspNetCore.Http;

namespace Sample.Filters.MyCustomAuthorization;

public class CustomAuthService(IHttpContextAccessor contextAccessor) : ICustomAuthService
{

    public Task<bool> CheckIfAllowed(string condition)
    {
        return Task.FromResult(contextAccessor.HttpContext?.User.Claims.Any() ?? false);
    }
}
