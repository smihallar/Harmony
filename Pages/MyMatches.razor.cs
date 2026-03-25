using Harmony.Services;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace Harmony.Pages
{
    public partial class MyMatches
    {
        private bool isConnectedToSpotify = false;
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IMatchService MatchService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string UserId { get; set; }
        List<MatchViewModel>? matchViewModels;
        bool isAuthorizedToView = false;
        List<string> errors = new List<string>();
        string matchMessage = string.Empty;
        bool isLoading = true;
  
        protected override async Task OnParametersSetAsync()
        {
            isAuthorizedToView = await AuthService.AuthorizeUser(UserId);
            if (!isAuthorizedToView)
            {
                var errorMessage = "You are not authorized to view this page.";
                var errorCode = HttpStatusCode.Forbidden;
                NavigationManager.NavigateTo($"/error?code={(int)errorCode}&message={Uri.EscapeDataString(errorMessage)}");
            }
            var user = await UserService.GetUserByIdAsync(UserId);
            isConnectedToSpotify = user.Data?.IsConnectedToSpotify ?? false;
            var response = await MatchService.GetMatchesAsync(UserId);
            var highScoreMatches = response.Data?.Where(m => m.CompatibilityScore >= 4).ToList() ?? new List<MatchViewModel>();
            if (highScoreMatches.Count > 0)
            {
                matchViewModels = highScoreMatches;

                // Clear previous errors if any
                errors = new List<string>();
                matchMessage = string.Empty;
            }
            if (response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                matchMessage = response.Message ?? "Failed to load matches.";
            }
            isLoading = false;
            StateHasChanged();
        }

        private async Task OnMatchesRefreshed()
        {
            // Reload the matches from your backend
            var response = await MatchService.GetMatchesAsync(UserId);
            var highScoreMatches = response.Data?.Where(m => m.CompatibilityScore >= 4).ToList() ?? new List<MatchViewModel>();
            matchViewModels = highScoreMatches;
            isLoading = false;
            StateHasChanged();
        }
    }
}
