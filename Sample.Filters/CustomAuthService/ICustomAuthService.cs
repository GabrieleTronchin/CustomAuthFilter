namespace Sample.Filters.CustomAuthService;

public interface ICustomAuthService
{
    Task<bool> CheckIfAllowed(string condition);
}