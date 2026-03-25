using Harmony.DTOs;
using Harmony.Viewmodels;

namespace Harmony.Services.Interfaces
{
    public interface ISpotifyService
    {
        Task<ReturnResponse<string>> GetSpotifyLoginUrl(string userId); // Get the login url to redirect user to spotify auth
        Task<ReturnResponse<UserProfileViewModel>> RefreshSpotifyTopItemsForUser(string userId); // Top items (song, artist, genre)
        Task<ReturnResponse<UserProfileViewModel>> RefreshUserProfileWithSpotifyDetails(string userId); // User details, ex spotifyuserid, profile pic.
    }
}
