using Blazored.LocalStorage;
using Harmony.Services.Authentication;
using Harmony.Services.Interfaces;
using System.Net.Http.Headers;

namespace Harmony.Services
{
    public class BaseHttpService
    {
        private readonly HttpClient client;
        protected readonly IAuthService authService;
        protected readonly ILocalStorageService localStorage;

        public BaseHttpService(HttpClient client, IAuthService authService, ILocalStorageService localStorage)
        {
            this.client = client;
            this.authService = authService;
            this.localStorage = localStorage;
        }

        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
