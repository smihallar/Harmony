using Harmony.DTOs;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ReturnResponse<bool>> AuthenticateAsync(LoginViewModel loginViewModel);
        Task<ReturnResponse> RegisterAsync(RegisterViewModel registerViewModel);
        Task LogoutAsync();
        Task<string> GetLoggedInUserIdAsync();
        Task<bool> AuthorizeUser(string userId);
    }
}
