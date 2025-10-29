using AutoMapper;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Mappings
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<MatchResponse, MatchViewModel>();
        }
    }
}
