using Blazored.LocalStorage;
using Harmony.Services.Interfaces;
namespace Harmony.Services
{
    public class UserService : BaseHttpService, IUserService
    {
        private readonly HttpClient client;
        private readonly IAuthService authService;
        private readonly ILocalStorageService localStorage;

        public UserService(HttpClient client, IAuthService authService, ILocalStorageService localStorage) : base(client, authService, localStorage)
        {
            this.client = client;
            this.authService = authService;
            this.localStorage = localStorage;
        }


    }
}
