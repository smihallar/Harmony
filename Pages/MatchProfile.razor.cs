using Harmony.Services;
using Harmony.Services.Authentication;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;

namespace Harmony.Pages
{
    public partial class MatchProfile : ComponentBase
    {
        private bool isConnectedToSpotify = false;
        private bool isLoading = true;
        private string matchProfileMessage = string.Empty;
        private List<string> errors = new List<string>();
        MatchViewModel? matchViewModel;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMatchService MatchService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        UserViewModel? CurrentUser;
        UserViewModel? InitiatorUser;
        UserViewModel? RecipientUser;
        [Parameter]
        public string MatchId { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            var loggedInUserId = await AuthService.GetLoggedInUserIdAsync();


            var response = await MatchService.GetMatchAsync(MatchId);
            if (response.Data != null)
            {
                matchViewModel = response.Data;
                errors = new List<string>();
                matchProfileMessage = string.Empty;

                var currentUserResponse = await UserService.GetUserByIdAsync(loggedInUserId);
                CurrentUser = currentUserResponse.Data;
                if(CurrentUser == null)
                {
                    errors = currentUserResponse.Errors.ToList();
                    isLoading = false;
                    StateHasChanged();
                    return;
                }
                if (CurrentUser.Id == matchViewModel.InitiatorUserId)
                {
                    InitiatorUser = CurrentUser;
                    var recipientResponse = await UserService.GetUserByIdAsync(matchViewModel.RecipientUserId);
                    RecipientUser = recipientResponse.Data;
                }
                else
                {
                    RecipientUser = CurrentUser;
                    var initiatorResponse = await UserService.GetUserByIdAsync(matchViewModel.InitiatorUserId);
                    InitiatorUser = initiatorResponse.Data;
                }
            }
            if (response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                matchProfileMessage = response.Message ?? "Failed to load match profile.";
            }
            isLoading = false;
            StateHasChanged();
        }
        public async Task DeleteMatch()
        {
            isLoading = true;
            StateHasChanged();
            if (matchViewModel != null)
            {
                await MatchService.DeleteMatchAsync(matchViewModel.MatchId);
                isLoading = false;
                NavigationManager.NavigateTo("/");
            }
            else
            {
                errors.Add("Match data is not available.");
            }
        }

    }
}
