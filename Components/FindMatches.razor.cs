using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Harmony.Components
{
    public partial class FindMatches
    {
        [Inject]
        public IMatchService MatchService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }

        [Parameter]
        public EventCallback OnMatchesRefreshed { get; set; }

        public async Task RefreshMatches()
        {
            var loggedInUser = await AuthService.GetLoggedInUserIdAsync();
            await MatchService.FindMatches(loggedInUser);

            await OnMatchesRefreshed.InvokeAsync();
            StateHasChanged();
        }
    }
}
