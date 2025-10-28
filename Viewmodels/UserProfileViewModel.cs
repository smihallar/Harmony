using Harmony.Services.Base;

namespace Harmony.Viewmodels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SpotifyUserId { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public bool IsSynthetic { get; set; }
        public bool IsConnectedToSpotify { get; set; }
        public DateTime MusicTasteLastRefreshed { get; set; }
        public List<SongViewModel> FavoriteSongs { get; set; } = new List<SongViewModel>();
        public List<ArtistViewModel> FavoriteArtists { get; set; } = new List<ArtistViewModel>();
        public List<GenreViewModel> FavoriteGenres { get; set; } = new List<GenreViewModel>();
    }
}
