using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample.Filters.CustomAuthService;

namespace Sample.With.ClassicAPI.CustomAuthAttribute;

public class MyCustomAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly string condition;

    public MyCustomAuthorizationFilter(string condition)
    {
        this.condition = condition;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var myCustomAuthService =
            context.HttpContext.RequestServices.GetRequiredService<ICustomAuthService>();

        if (!await myCustomAuthService.CheckIfAllowed(condition))
        {
            context.Result = new ForbidResult();
        }
    }
}
