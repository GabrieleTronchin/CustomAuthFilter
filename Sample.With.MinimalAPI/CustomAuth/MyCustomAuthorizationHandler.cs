using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Filters.MyCustomAuthorization;

namespace Sample.With.MinimalAPI.CustomAuth;

public class MyCustomAuthorizationHandler(ICustomAuthService customAuthService) : AuthorizationHandler<MyCustomAuthRequirementInput>
{

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyCustomAuthRequirementInput requirement)
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