using Harmony.Services;
using Harmony.Services.Authentication;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.WebUtilities;

namespace Harmony.Pages
{
    public partial class Error
    {
        [Parameter]
        public string ErrorCode { get; set; } = string.Empty;
        [Parameter]
        public string ErrorMessage { get; set; } = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);

            if (query.TryGetValue("code", out var code))
                ErrorCode = code;

            if (query.TryGetValue("message", out var message))
                ErrorMessage = message;
        }
    }

}
