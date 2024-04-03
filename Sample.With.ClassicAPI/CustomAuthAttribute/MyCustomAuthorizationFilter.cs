using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Filters.MyCustomAuthorization;

public class MyCustomAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly string condition;

    public MyCustomAuthorizationFilter(string condition)
    {
        this.condition = condition;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var myCustomAuthService = context.HttpContext.RequestServices.GetRequiredService<ICustomAuthService>();

        if (!await myCustomAuthService.CheckIfAllowed(condition))
        {
            context.Result = new ForbidResult();
        }
    }
}
