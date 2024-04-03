namespace Sample.Filters.MyCustomAuthorization;

public interface ICustomAuthService
{
    Task<bool> CheckIfAllowed(string condition);
}