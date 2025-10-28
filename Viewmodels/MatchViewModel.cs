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
    }
}
