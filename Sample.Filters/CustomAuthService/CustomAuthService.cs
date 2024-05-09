using Microsoft.AspNetCore.Http;

namespace Sample.Filters.CustomAuthService;

public class CustomAuthService(IHttpContextAccessor contextAccessor) : ICustomAuthService
{
    public Task<bool> CheckIfAllowed(string condition)
    {
        //TODO Add your custom pocily
        return Task.FromResult(true);
    }
}
