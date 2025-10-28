using Harmony.Services.Base;

namespace Harmony.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(UserLoginRequest loginModel);
        Task Logout();
    }
}
