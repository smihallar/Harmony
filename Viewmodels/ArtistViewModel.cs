using Harmony.Services.Base;

namespace Harmony.Viewmodels
{
    public class ArtistViewModel
    {
        public string ArtistId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ArtistImageUrl { get; set; } = string.Empty;
        public string SpotifyId { get; set; } = string.Empty;
        public int? Popularity { get; set; }
        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();

    }
}
