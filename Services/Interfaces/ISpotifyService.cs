using Harmony.DTOs;
using Harmony.Viewmodels;

namespace Harmony.Services.Interfaces
{
    public interface ISpotifyService
    {
        Task<ReturnResponse<string>> GetSpotifyLoginUrl(); // Get the login url to redirect user to spotify auth
        Task<ReturnResponse<UserProfileViewModel>> RefreshSpotifyTopItemsForUser(); // Top items (song, artist, genre)
        Task<ReturnResponse<UserProfileViewModel>> RefreshUserProfileWithSpotifyDetails(); // User details, ex spotifyuserid, profile pic.
    }
}
