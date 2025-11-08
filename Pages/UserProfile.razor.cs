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
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string UserId { get; set; }
        UserProfileViewModel? userProfileModel;
        bool isAuthorizedToEdit = false;
        List<string> errors = new List<string>();
        string profileMessage = string.Empty;

        protected override async Task OnParametersSetAsync()
        {

            isAuthorizedToEdit = await AuthService.AuthorizeUser(UserId);
            var response = await UserService.GetUserProfileAsync(UserId);
            if (response.Data != null)
            {
                userProfileModel = response.Data;
            }
            if(response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                profileMessage = response.Message ?? "Failed to load user profile.";
            }
            StateHasChanged();
        }

        public async Task SaveBioAsync()
        {
            var response = await UserService.UpdateUserBioAsync(UserId, userProfileModel?.Biography ?? string.Empty);
            if (response != null && response.Errors != null && response.Errors.Any())
            {
                errors = response.Errors.ToList();
                profileMessage = response.Message ?? "Failed to update biography.";
            }
            else
            {
                userProfileModel = response?.Data;
            }
                
            StateHasChanged();
        }
    }
}
