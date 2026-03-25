using Harmony.Services;
using Harmony.Services.Authentication;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Harmony.Pages
{
    public partial class UserProfile : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public ISpotifyService SpotifyService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string UserId { get; set; }
        UserProfileViewModel? userProfileModel;
        bool isAuthorizedToEdit = false;
        List<string> errors = new List<string>();
        string profileMessage = string.Empty;
        string updateBioMessage = string.Empty;
        bool isLoading = true;
        private bool isEditingBio = false;


        protected override async Task OnParametersSetAsync()
        {

            isAuthorizedToEdit = await AuthService.AuthorizeUser(UserId);
            var response = await UserService.GetUserProfileAsync(UserId);
            if (response.Data != null)
            {
                userProfileModel = response.Data;
                errors = new List<string>();
                profileMessage = string.Empty;
            }
            if(response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                profileMessage = response.Message ?? "Failed to load user profile.";
            }
            isLoading = false;
            StateHasChanged();
        }

        public async Task SaveBioAsync()
        {
            isLoading = true;
            StateHasChanged();
            var response = await UserService.UpdateUserBioAsync(UserId, userProfileModel?.Biography ?? string.Empty);
            if (response != null && response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                updateBioMessage = response.Message ?? "Failed to update biography.";
                isEditingBio = false;
            }
            else if(response == null || response.Data == null)
            {
                isEditingBio = false;
                updateBioMessage = "Something went wrong while updating your biography. Please try again.";
            }
            else
            {
                userProfileModel.Biography = response.Data;
                updateBioMessage = string.Empty;
                isEditingBio = false;
            }
            StateHasChanged();
        }

        public async Task DeleteUser()
        {
            isLoading = true;
            StateHasChanged();
            var response = await UserService.DeleteUserAsync(UserId);
            if(response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                profileMessage = response.Message ?? "Failed to delete user.";
                isLoading = false;
                StateHasChanged();
                return;
            }
            await AuthService.LogoutAsync();
            NavigationManager.NavigateTo("/");
        }
    }
}
