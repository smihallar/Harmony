using Harmony.Services.Authentication;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;
using System.Transactions;

namespace Harmony.Components
{
    public partial class LoginForm
    {
        LoginViewModel LoginModel { get; set; } = new LoginViewModel();
        string userId = string.Empty;
        string loginMessage = string.Empty;
        List<string> errors = new List<string>();
        [Inject]
        public IAuthService AuthService { get; set; } 
        [Inject]
        private NavigationManager navigationManager { get; set; }
        

        public async Task HandleLogin()
        {
            var loginSuccessful = await AuthService.AuthenticateAsync(LoginModel);

            if (loginSuccessful.Data)
            {
                StateHasChanged();
                userId = await AuthService.GetLoggedInUserIdAsync();
                if(!string.IsNullOrEmpty(userId))
                {
                    navigationManager.NavigateTo($"/user-profile/{userId}");
                }
                else
                {
                    navigationManager.NavigateTo("/");
                }
            }
            else             
            {
                loginMessage = loginSuccessful.Message ?? "Login failed. Please try again.";
                errors = loginSuccessful.Errors?.ToList() ?? new List<string>();
                StateHasChanged();
            }
        }
    }
}
