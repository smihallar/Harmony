using Blazored.LocalStorage;
using Harmony.Services.Authentication;
using Harmony.Services.Base;
using Harmony.Services.Interfaces;
using System.Net.Http.Headers;

namespace Harmony.Services
{
    public class BaseHttpService
    {
        private readonly IClient client;
        protected readonly IAuthService authService;
        protected readonly ILocalStorageService localStorage;

        public BaseHttpService(IClient client, IAuthService authService, ILocalStorageService localStorage)
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
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
