using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;

namespace Harmony.Components
{
    public partial class UserHomePage
    {
        UserProfileViewModel? userProfileModel;
        [Inject]
        public IUserService UserService { get; set; }
        string loggedInUserId;

    }
}
