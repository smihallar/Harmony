using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Harmony.Pages
{
    public partial class Home : ComponentBase
    {
        bool isLoading = true;
        private bool isConnectedToSpotify = false;
        [Inject]
        private IAuthService AuthService { get; set; }
        [Inject]
        private IUserService UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var loggedInUserId = await AuthService.GetLoggedInUserIdAsync();
            if (loggedInUserId != null)
            {
                var user = await UserService.GetUserByIdAsync(loggedInUserId);
                if (user.Data != null)
                {
                    isConnectedToSpotify = user.Data.IsConnectedToSpotify;
                    isLoading = false;
                    StateHasChanged();
                }
            }
        }
    }
}
