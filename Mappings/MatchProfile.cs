using AutoMapper;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Mappings
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<MatchResponse, MatchViewModel>()
                .ForMember(dest => dest.MutualSongs, opt => opt.MapFrom(src => src.MutualSongs))
                .ForMember(dest => dest.MutualArtists, opt => opt.MapFrom(src => src.MutualArtists))
                .ForMember(dest => dest.MutualGenres, opt => opt.MapFrom(src => src.MutualGenres));
        }
    }
}
