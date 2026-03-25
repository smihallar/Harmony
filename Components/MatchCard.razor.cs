using Harmony.Services;
using Harmony.Services.Interfaces;
using Harmony.Viewmodels;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace Harmony.Components
{
    public partial class MatchCard : ComponentBase
    {
        [Inject]
        private IMatchService MatchService { get; set; }
        [Inject]
        private IUserService UserService { get; set; }
        [Inject]
        private IAuthService AuthService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Parameter]
        public MatchViewModel MatchViewModel { get; set; }
        UserViewModel? InitiatorUser;
        UserViewModel? RecipientUser;
        UserViewModel? DisplayProfile;
        List<ArtistViewModel>? DisplayArtists;
        List<SongViewModel>? DisplaySongs;
        List<GenreViewModel>? DisplayGenres;
        bool isInitiator = false;


        protected override async Task OnParametersSetAsync()
        {
            var initiatorResponse = await UserService.GetUserByIdAsync(MatchViewModel.InitiatorUserId);
            var recipientResponse = await UserService.GetUserByIdAsync(MatchViewModel.RecipientUserId);
            var loggedInUserId = await AuthService.GetLoggedInUserIdAsync();

            if (initiatorResponse.Data != null && recipientResponse.Data != null)
            {
                InitiatorUser = initiatorResponse.Data;
                RecipientUser = recipientResponse.Data;
                isInitiator = InitiatorUser.Id == loggedInUserId;
                if(isInitiator)
                {
                    DisplayProfile = RecipientUser;
                }
                else
                {
                    DisplayProfile = InitiatorUser;
                }
                var displayArtistsList = MatchViewModel.MutualArtists.Where(a => !string.IsNullOrEmpty(a.ArtistImageUrl));

                //Use artists and songs with images for display
                if(displayArtistsList.Any())
                {
                    if(displayArtistsList.Count() > 2)
                        DisplayArtists = displayArtistsList.Take(2).ToList(); // Max 2 artists to display
                    else
                        DisplayArtists = displayArtistsList.ToList();
                }
                var displaySongsList = MatchViewModel.MutualSongs.Where(s => !string.IsNullOrEmpty(s.AlbumImageUrl));
                if (displaySongsList.Any())
                {
                    if(displaySongsList.Count() > 2)
                        DisplaySongs = displaySongsList.Take(2).ToList(); // Max 2 songs to display
                    else
                        DisplaySongs = displaySongsList.ToList();
                }
                var displayGenresList = MatchViewModel.MutualGenres;
                if(displayGenresList.Any())
                {
                    if(displayGenresList.Count() > 4)
                        DisplayGenres = displayGenresList.Take(4).ToList(); // Max 4 genres to display
                    else
                        DisplayGenres = displayGenresList.ToList();
                }
            }
            else
            {
                var errorCode = HttpStatusCode.NotFound;
                var errorMessage = "Something went wrong while displaying matches. One or more users could not be found";
                NavigationManager.NavigateTo($"/error?code={(int)errorCode}&message={Uri.EscapeDataString(errorMessage)}");
            }
            

        }

        public async Task DeleteMatch()
        {
            await MatchService.DeleteMatchAsync(MatchViewModel.MatchId);
            StateHasChanged();
        }

        public void NavigateToMatchProfile()
        {
           NavigationManager.NavigateTo($"/match-profile/{MatchViewModel.MatchId}");
        }
    }
}
