using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Harmony.Components
{
    public partial class ConnectToSpotify
    {
        [Inject]
        public ISpotifyService SpotifyService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        List<string> Errors = new List<string>();
        string errorMessage = string.Empty;

        public async Task ConnectUserToSpotify()
        {
            var loggedInUserId = await AuthService.GetLoggedInUserIdAsync();
            var urlResponse = await SpotifyService.GetSpotifyLoginUrl(loggedInUserId);
            if (urlResponse.Errors != null && urlResponse.Errors.Count > 0)
            {
                Errors = urlResponse.Errors.ToList();
                errorMessage = urlResponse.Message ?? "Failed to connect to Spotify.";
            }
            else if(urlResponse.Data != null)
            {
                await JSRuntime.InvokeVoidAsync("open", urlResponse.Data, "_self");
            }
            else
            {
                Errors = new List<string> { "Unknown error occurred." };
                errorMessage = "Failed to connect to Spotify.";
            }
        }
    }
}
