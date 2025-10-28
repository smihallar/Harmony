using Blazored.LocalStorage;
using Harmony.Providers;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace Harmony.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(UserLoginRequest loginRequest)
        {
            // Call the login endpoint
            var response = await httpClient.LoginAsync(loginRequest);

            // Store the token received from API in local storage
            await localStorage.SetItemAsync("accessToken", response.Data.Token);

            // Change auth state of application, using the ApiAuthenticationStateProvider
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }

        public async Task<ReturnResponse> Register(UserRegisterRequest registerRequest)
        {
            try
            {
                var response = await httpClient.RegisterAsync(registerRequest);
                if (response.Errors != null && response.Errors.Count > 0)
                {
                    return new ReturnResponse
                    {
                        Errors = response.Errors,
                        Message = "Registration failed",
                        StatusCode = response.StatusCode
                    };
                }
                return new ReturnResponse
                {
                    Message = "Registration successful",
                    StatusCode = response.StatusCode
                };
            }
            catch (Exception)
            {
                return new ReturnResponse
                {
                    Message = "An error occurred during registration",
                    StatusCode = HttpStatusCode._500
                };
            }
        }
    }
}
