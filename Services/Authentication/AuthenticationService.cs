using Blazored.LocalStorage;
using Harmony.Providers;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace Harmony.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(UserLoginRequest loginModel)
        {
            // Call the login endpoint
            var response = await httpClient.LoginPOSTAsync(loginModel);

            // Store the token received from API in local storage
            await localStorage.SetItemAsync("accessToken", response.Token);

            // Change auth state of application, using the ApiAuthenticationStateProvider
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
