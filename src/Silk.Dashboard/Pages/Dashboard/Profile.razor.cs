using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Silk.Dashboard.Models.Discord;

namespace Silk.Dashboard.Pages.Dashboard
{
    public partial class Profile : ComponentBase
    {
        private const ulong DiscordManageServerPermission = 0x20;

        private string _token;
        private bool _oAuthTokenVisible;

        private DiscordApiUser _user;
        private List<DiscordApiGuild> AllGuilds { get; set; }
        private List<DiscordApiGuild> OwnedGuilds { get; set; }

        private string OAuthTokenVisibility => _oAuthTokenVisible ? "text" : "password";

        protected override async Task OnInitializedAsync()
        {
            _user = await UserService.GetUserInfoAsync(HttpContextAccessor.HttpContext);
            _token = await UserService.GetTokenAsync(HttpContextAccessor.HttpContext);

            AllGuilds = await UserService.GetUserGuildsAsync(HttpContextAccessor.HttpContext);
            OwnedGuilds = AllGuilds.Where(GuildIsOwnedByUser).ToList();
        }

        private static bool GuildIsOwnedByUser(DiscordApiGuild guild)
        {
            return (guild.Permissions & DiscordManageServerPermission) == DiscordManageServerPermission;
        }

        private void ToggleOAuthTokenVisibility()
        {
            _oAuthTokenVisible = !_oAuthTokenVisible;
        }
    }
}