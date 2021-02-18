using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Components;
using Silk.Dashboard.Models.Discord;
using Silk.Extensions;

namespace Silk.Dashboard.Pages.Dashboard
{
    public partial class Profile : ComponentBase
    {
        private const ulong DiscordManageServerPermission = 0x20;

        private string _token;
        private bool _oAuthTokenVisible;

        private bool _doneLoading;
        private DiscordRestClient _client;
        private IReadOnlyList<DiscordGuild> _allGuilds;
        private IReadOnlyList<DiscordGuild> _ownedGuilds;

        private string OAuthTokenVisibility => _oAuthTokenVisible ? "text" : "password";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                _token = await UserService.GetTokenAsync(HttpContextAccessor.HttpContext);

                var config = new DiscordConfiguration
                {
                    Token = _token,
                    TokenType = TokenType.Bearer
                };
                
                _client = new DiscordRestClient(config); // This breaks things

                _allGuilds = _client.Guilds.Values.ToList(); // await _client.GetCurrentUserGuildsAsync(100, 0, 0);
                _ownedGuilds = _allGuilds.Where(g => 
                    g.Members.First(m => m.Value.Id == _client.CurrentUser.Id)
                        .Value.Roles.Any(r => r.HasPermission(Permissions.ManageGuild)))
                    .ToList();

                _doneLoading = true;
                StateHasChanged();
            }
        }

        private void ToggleOAuthTokenVisibility()
        {
            _oAuthTokenVisible = !_oAuthTokenVisible;
        }
    }
}