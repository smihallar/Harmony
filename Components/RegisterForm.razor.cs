using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;
using System.Net;
namespace Harmony.Components
{
    public partial class RegisterForm
    {
        RegisterViewModel RegisterModel { get; set; } = new RegisterViewModel();
        string errorMessage = string.Empty;
        string successMessage = string.Empty;
        List<string> errors = new List<string>();
        [Inject]
        public IAuthService AuthService { get; set; }

        public async Task HandleRegister()
        {
            var registerResponse = await AuthService.RegisterAsync(RegisterModel);

            if ((int)registerResponse.StatusCode == 200)
            {
                successMessage = registerResponse.Message;
                errorMessage = string.Empty;
                errors = new List<string>();
                StateHasChanged();
            }
            else
            {
                errorMessage = registerResponse.Message;
                errors = registerResponse.Errors != null ? registerResponse.Errors.ToList() : new List<string>();
                successMessage = string.Empty;
            }
            StateHasChanged();
        }
    }
}
