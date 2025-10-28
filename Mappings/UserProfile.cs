using AutoMapper;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginViewModel, UserLoginRequest>();
            CreateMap<RegisterViewModel, UserRegisterRequest>();
            CreateMap<UserProfileResponse, UserProfileViewModel>();
            CreateMap<UserProfileViewModel, UpdateUserBioRequest>()
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Biography));
        }
    }
}
