using AutoMapper;
using Blazored.LocalStorage;
using Harmony.DTOs;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using static Harmony.Services.Base.Client;

namespace Harmony.Services
{
    public class SpotifyService : BaseHttpService, ISpotifyService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public SpotifyService(IClient client, IAuthService authService, ILocalStorageService localStorage, IMapper mapper) : base(client, authService, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<ReturnResponse<string>> GetSpotifyLoginUrl(string userId)
        {
            try
            {
                //await GetBearerToken();
                var response = await client.LoginSpotifyAsync(userId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<string>
                    {
                        Errors = response.Errors,
                        Message = response.Message
                    };
                }
                
                return new ReturnResponse<string>
                {
                    Data = response.Data.AuthorizationUrl
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<string>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while connecting user to spotify"
                };
            }
        }

        public async Task<ReturnResponse<UserProfileViewModel>> RefreshSpotifyTopItemsForUser()
        {
            try
            {
                await GetBearerToken();
                var response = await client.RefreshTopItemsAsync();
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<UserProfileViewModel>
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                var userProfileViewModel = mapper.Map<UserProfileViewModel>(response.Data);
                return new ReturnResponse<UserProfileViewModel>
                {
                    Data = userProfileViewModel
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<UserProfileViewModel>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while refreshing top items"
                };
            }
        }

        public async Task<ReturnResponse<UserProfileViewModel>> RefreshUserProfileWithSpotifyDetails()
        {
            try
            {
                await GetBearerToken();
                var response = await client.RefreshProfileAsync();
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<UserProfileViewModel>
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                var userProfileViewModel = mapper.Map<UserProfileViewModel>(response.Data);
                return new ReturnResponse<UserProfileViewModel>
                {
                    Data = userProfileViewModel
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<UserProfileViewModel>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while refreshing user profile"
                };
            }
        }
    }
}
