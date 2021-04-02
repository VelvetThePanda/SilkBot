using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Components;
using Silk.Dashboard.Services;

namespace Silk.Dashboard.Pages.Dashboard
{
    public partial class Profile : ComponentBase
    {
        [Inject] private DiscordRestClientService RestClientService { get; set; }

        private IReadOnlyList<DiscordGuild> _ownedGuilds;

        protected override async Task OnInitializedAsync()
        {
            var allGuilds = await RestClientService.GetAllGuildsAsync();
            _ownedGuilds = RestClientService.FilterGuildsByPermission(allGuilds, Permissions.ManageGuild);
        }

        private string CurrentUserAvatar => RestClientService.RestClient.CurrentUser.GetAvatarUrl(ImageFormat.Auto);
        private string CurrentUserName => RestClientService.RestClient.CurrentUser.Username;
        private string HeaderViewGreeting => $"Hello, {CurrentUserName}";
    }
}