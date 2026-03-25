using Harmony.DTOs;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReturnResponse<UserViewModel>> GetUserByIdAsync(string userId);
        Task<ReturnResponse> DeleteUserAsync(string userId);
        Task<ReturnResponse<string>> UpdateUserBioAsync(string userId, string newBio);
        Task<ReturnResponse<UserProfileViewModel>> GetUserProfileAsync(string userId);
    }
}
