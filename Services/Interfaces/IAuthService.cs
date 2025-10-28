using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(LoginViewModel loginViewModel);
        Task<ReturnResponse> Register(RegisterViewModel registerViewModel);
        Task Logout();
    }
}
