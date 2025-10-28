namespace Harmony.Viewmodels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SpotifyUserId { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public bool IsSynthetic { get; set; } = false;
        public bool IsConnectedToSpotify { get; set; } = false;
        public DateTime MusicTasteLastRefreshed { get; set; } = DateTime.UtcNow;
        public List<string> FavoriteSongIds { get; set; } = new List<string>();
        public List<string> FavoriteArtistIds { get; set; } = new List<string>();
        public List<string> FavoriteGenreIds { get; set; } = new List<string>();
    }
}
