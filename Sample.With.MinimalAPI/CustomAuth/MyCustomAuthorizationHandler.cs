using Microsoft.AspNetCore.Authorization;
using Sample.Filters.CustomAuthService;

namespace Sample.With.MinimalAPI.CustomAuth;

public class MyCustomAuthorizationHandler(ICustomAuthService customAuthService)
    : AuthorizationHandler<MyCustomAuthRequirementInput>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MyCustomAuthRequirementInput requirement
    )
    {
        if (await customAuthService.CheckIfAllowed(requirement.Condition))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
