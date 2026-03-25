using Harmony.DTOs;
using Harmony.Services.Base;
using Harmony.Viewmodels;

namespace Harmony.Services.Interfaces
{
    public interface IMatchService
    {
        Task<ReturnResponse<List<MatchViewModel>>> GetMatchesAsync(string userId);
        Task<ReturnResponse> DeleteMatchAsync(string matchId);
        Task<ReturnResponse<MatchViewModel>> GetMatchAsync(string matchId);
        Task<ReturnResponse<List<MatchViewModel>>> FindMatches(string userId);
    }
}
