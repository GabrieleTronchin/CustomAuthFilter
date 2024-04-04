# Authorization Filter

Authorization Filter in .NET are used to verify that a user is authorized to access a particular controller.

This project illustrates the implementation of an Authorization filter in C#.NET, showcasing two ASP.NET API approaches: 
- Classic API utilizing controllers
- Minimal API.

## Moq CustomAuthService

'CustomAuthService' resides within a project library named "Sample.Filters".
It serves as an example of an authentication service. 
Its purpose is to parse the token and subsequently execute a custom logic to validate the request.

## Classic API

Below is the creation of a custom auth attribute:

```csharp
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
```
Once defined, you can apply it atop your controllers:

```csharp
[MyCustomAuthorizationFilter("Test")]
```

## Minimal API

To craft a custom auth attribute with Minimal API, certain prerequisites must be established:

```csharp
public class MyCustomAuthRequirementInput : IAuthorizationRequirement
{
    public MyCustomAuthRequirementInput(string condition) => Condition = condition;
    public string Condition { get; set; }
}
```

Subsequently, a custom AuthorizationHandler needs implementation with the aforementioned input:

```csharp
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
```
Finally, adjust your program.cs as follows:

```csharp
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Test", p => p.AddRequirements(new MyCustomAuthRequirementInput("Test")));
});

// .......

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.RequireAuthorization("Test")
.WithOpenApi();
```csharp
