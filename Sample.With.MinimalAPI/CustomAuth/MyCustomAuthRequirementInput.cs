using Microsoft.AspNetCore.Authorization;

namespace Sample.With.MinimalAPI.CustomAuth;

public class MyCustomAuthRequirementInput : IAuthorizationRequirement
{
    public MyCustomAuthRequirementInput(string condition) => Condition = condition;
    public string Condition { get; set; }
}