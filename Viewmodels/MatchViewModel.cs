namespace Harmony.Viewmodels
{
    public class MatchViewModel
    {

        public string MatchId { get; set; } = string.Empty;
        public string InitiatorUserId { get; set; } = string.Empty;
        public string RecipientUserId { get; set; } = string.Empty;
        public int CompatibilityScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> MutualSongIds { get; set; } = new List<string>();
        public List<string> MutualArtistIds { get; set; } = new List<string>();
        public List<string> MutualGenreIds { get; set; } = new List<string>();
        public List<SongViewModel> MutualSongs { get; set; } = new List<SongViewModel>();
        public List<ArtistViewModel> MutualArtists { get; set; } = new List<ArtistViewModel>();
        public List<GenreViewModel> MutualGenres { get; set; } = new List<GenreViewModel>();
    }
}
