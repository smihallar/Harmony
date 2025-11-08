using AutoMapper;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Core maps
            CreateMap<Genre, GenreViewModel>();

            CreateMap<Artist, ArtistViewModel>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));

            CreateMap<Song, SongViewModel>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists));

            // User auth maps
            CreateMap<LoginViewModel, UserLoginRequest>();
            CreateMap<RegisterViewModel, UserRegisterRequest>();

            // User profile maps
            CreateMap<UserProfileResponse, UserProfileViewModel>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt == default ? DateTime.UtcNow : src.CreatedAt));

            CreateMap<UserProfileViewModel, UpdateUserBioRequest>()
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Biography));

        }
    }
}
