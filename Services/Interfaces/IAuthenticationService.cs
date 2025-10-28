using Harmony.Services.Base;

namespace Harmony.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UserLoginRequest loginModel);
        Task Logout();
    }
}
