using Harmony.Services.Base;

namespace Harmony.Viewmodels
{
    public class SongViewModel
    {
        public string SongId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string AlbumImageUrl { get; set; } = string.Empty;
        public int? Popularity { get; set; }
        public string SpotifyId { get; set; } = string.Empty;
        public List<ArtistViewModel> Artists { get; set; } = new List<ArtistViewModel>();

    }
}
