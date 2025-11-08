using AutoMapper;
using Blazored.LocalStorage;
using Harmony.DTOs;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
namespace Harmony.Services
{
    public class UserService : BaseHttpService, IUserService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public UserService(IClient client, IAuthService authService, ILocalStorageService localStorage, IMapper mapper) : base(client, authService, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<ReturnResponse<UserViewModel>> GetUserByIdAsync(string userId)
        {
            try
            {
                var response = await client.GetUserAsync(userId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<UserViewModel>
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                var userViewModel = mapper.Map<UserViewModel>(response.Data);
                return new ReturnResponse<UserViewModel>
                {
                    Data = userViewModel
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<UserViewModel>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while fetching the user."
                };
            }
        }

        public async Task<ReturnResponse> DeleteUserAsync(string userId)
        {
            try
            {
                await GetBearerToken(); // Ensure the bearer token is set before making the request
                var response = await client.DeleteUserAsync(userId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                return new ReturnResponse
                {
                    Message = "User deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while deleting the user."
                };
            }
        }

        public async Task<ReturnResponse<UserProfileViewModel>> UpdateUserBioAsync(string userId, string newBio)
        {
            try
            {
                if (string.IsNullOrEmpty(newBio)|| newBio.Length > 500)
                {
                    return new ReturnResponse<UserProfileViewModel>
                    {
                        Errors = new List<string> { "Bio must be between 0 and 500 characters." },
                        Message = "Invalid biography length."
                    };
                }
                var updateRequest = new UpdateUserBioRequest { Bio = newBio };
                await GetBearerToken(); // Ensure the bearer token is set before making the request
                var response = await client.UpdateBioAsync(userId, updateRequest);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<UserProfileViewModel>
                    {
                        StatusCode = response.StatusCode,
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
                    StatusCode = HttpStatusCode._500,
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while updating the user bio."
                };
            }
        }

        public async Task<ReturnResponse<UserProfileViewModel>> GetUserProfileAsync(string userId)
        {
            try
            {
                var response = await client.GetProfileAsync(userId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<UserProfileViewModel>
                    {
                        StatusCode = response.StatusCode,
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
                    StatusCode = HttpStatusCode._500,
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while fetching the user profile."
                };
            }
        }
    }
}
