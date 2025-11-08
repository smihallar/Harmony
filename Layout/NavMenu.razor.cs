using Harmony.Services.Authentication;
using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Harmony.Layout
{
    public partial class NavMenu
    {
        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; } = null!;
        [Inject]
        private IAuthService authService { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        private bool collapseNavMenu = true;
        private string? userId = null;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnParametersSetAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            ClaimsPrincipal user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                userId = user.FindFirst("uid")?.Value;
            }
            else
            {
                userId = null;
            }

            StateHasChanged();
        }

        private async Task Logout()
        {
             await authService.LogoutAsync();
        }
        private async Task NavigateToUserProfile()
        {
            var loggedInUser = await authService.GetLoggedInUserIdAsync();
            if (loggedInUser != null)
            {
                userId = loggedInUser;
                NavigationManager.NavigateTo($"/user-profile/{userId}");
            }
        }
        private async Task NavigateToMatches()
        {
            var loggedInUser = await authService.GetLoggedInUserIdAsync();
            if (loggedInUser != null)
            {
                userId = loggedInUser;
                NavigationManager.NavigateTo($"/matches/{userId}");
            }
        }
    }
}
