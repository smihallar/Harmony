using AutoMapper;
using Blazored.LocalStorage;
using Harmony.DTOs;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;

namespace Harmony.Services
{
    public class MatchService : BaseHttpService, IMatchService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public MatchService(IClient client, IAuthService authService, ILocalStorageService localStorage, IMapper mapper) : base(client, authService, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<ReturnResponse> DeleteMatchAsync(string matchId)
        {
            try
            {
                await GetBearerToken(); // Ensure the bearer token is set before making the request
                var response = await client.DeleteMatchAsync(matchId);
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
                    Message = "Match deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while deleting the match."
                };
            }
        }

        public async Task<ReturnResponse<List<MatchViewModel>>> FindMatches(string userId)
        {
            try
            {
                await GetBearerToken(); // Ensure the bearer token is set before making the request
                var response = await client.FindMatchesAsync(userId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<List<MatchViewModel>>
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                var matchViewModels = mapper.Map<List<MatchViewModel>>(response.Data);
                return new ReturnResponse<List<MatchViewModel>>
                {
                    Data = matchViewModels
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<List<MatchViewModel>>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while finding matches."
                };
            }
        }

        public async Task<ReturnResponse<MatchViewModel>> GetMatchAsync(string matchId)
        {
            try
            {
                await GetBearerToken(); // Ensure the bearer token is set before making the request
                var response = await client.GetMatchAsync(matchId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<MatchViewModel>
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                var matchViewModel = mapper.Map<MatchViewModel>(response.Data);
                return new ReturnResponse<MatchViewModel>
                {
                    Data = matchViewModel
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<MatchViewModel>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while fetching the match."
                };
            }
        }

        public async Task<ReturnResponse<List<MatchViewModel>>> GetMatchesAsync(string userId)
        {
            try
            {
                await GetBearerToken(); // Ensure the bearer token is set before making the request
                var response = await client.GetAllMatchesAsync(userId);
                if (response.Errors != null && response.Errors.Any())
                {
                    return new ReturnResponse<List<MatchViewModel>>
                    {
                        Errors = response.Errors,
                        Message = response.Message,
                    };
                }
                var matchViewModels = mapper.Map<List<MatchViewModel>>(response.Data);
                return new ReturnResponse<List<MatchViewModel>>
                {
                    Data = matchViewModels
                };
            }
            catch (Exception ex)
            {
                return new ReturnResponse<List<MatchViewModel>>
                {
                    Errors = new List<string> { ex.Message },
                    Message = "An error occurred while fetching matches."
                };
            }
        }
    }
}
