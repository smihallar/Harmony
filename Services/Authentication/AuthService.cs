using AutoMapper;
using Blazored.LocalStorage;
using Harmony.DTOs;
using Harmony.Providers;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace Harmony.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly IMapper mapper;

        public AuthService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
            this.mapper = mapper;
        }

        public async Task<ReturnResponse<bool>> AuthenticateAsync(LoginViewModel loginViewModel)
        {
            try
            {
                // Call the login endpoint
                var loginRequest = mapper.Map<UserLoginRequest>(loginViewModel);
                var response = await httpClient.LoginAsync(loginRequest);
                switch (response.StatusCode)
                {
                    case HttpStatusCode._400:
                        return new ReturnResponse<bool>
                        {
                            Message = response.Message,
                            Errors = response.Errors,
                            StatusCode = response.StatusCode,
                            Data = false
                        };
                    case HttpStatusCode._500:
                        return new ReturnResponse<bool>
                        {
                            Message = response.Message,
                            Errors = response.Errors,
                            StatusCode = response.StatusCode,
                            Data = false
                        };
                    default:
                        var token = response.Data.Token;
                        await localStorage.SetItemAsync("accessToken", token);

                        var handler = new JwtSecurityTokenHandler();
                        var jwt = handler.ReadJwtToken(token);
                        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

                        if (!string.IsNullOrEmpty(userId))
                            await localStorage.SetItemAsync("userId", userId);

                        await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

                        return new ReturnResponse<bool>
                        {
                            Message = response.Message,
                            Errors = response.Errors,
                            StatusCode = response.StatusCode,
                            Data = true
                        };

                }
            }
            catch (Exception ex)
            {
                return new ReturnResponse<bool>
                {
                    Message = "An error occurred during authentication",
                    StatusCode = HttpStatusCode._500,
                    Errors = new List<string> { ex.Message },
                    Data = false
                };
            }
        }

        public async Task LogoutAsync()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }

        public async Task<ReturnResponse> RegisterAsync(RegisterViewModel registerViewModel)
        {
            try
            {
                var registerRequest = mapper.Map<UserRegisterRequest>(registerViewModel);
                var response = await httpClient.RegisterAsync(registerRequest);
                switch (response.StatusCode)
                {
                    case HttpStatusCode._400:
                        return new ReturnResponse
                        {
                            Errors = response.Errors,
                            Message = response.Message,
                            StatusCode = response.StatusCode
                        };
                    case HttpStatusCode._500:
                        return new ReturnResponse
                        {
                            Errors = response.Errors,
                            Message = response.Message,
                            StatusCode = response.StatusCode
                        };
                    default: // 200 OK
                        return new ReturnResponse
                        {
                            Message = "Registration successful!",
                            StatusCode = response.StatusCode
                        };
                }
            }
            catch (Exception)
            {
                return new ReturnResponse
                {
                    Message = "An error occurred during registration",
                    Errors = new List<string> { "Something went wrong, please try again later." },
                    StatusCode = HttpStatusCode._500
                };
            }
        }

        public async Task<string> GetLoggedInUserIdAsync()
        {
            var userId = await localStorage.GetItemAsync<string>("userId");
            return userId ?? string.Empty;
        }

        public async Task<bool> AuthorizeUser(string userId)
        {
            var loggedInUserId = await GetLoggedInUserIdAsync();
            if (loggedInUserId == userId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
