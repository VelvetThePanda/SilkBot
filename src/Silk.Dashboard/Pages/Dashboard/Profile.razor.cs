using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Components;

namespace Silk.Dashboard.Pages.Dashboard
{
    public partial class Profile : ComponentBase
    {
        private IReadOnlyList<DiscordGuild> _allGuilds;
        private IReadOnlyList<DiscordGuild> _ownedGuilds;
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                _allGuilds = await RestClientService.GetAllGuildsAsync();

                _ownedGuilds = RestClientService.GetGuildsByPermission(Permissions.ManageGuild);

                StateHasChanged();
            }
        }
    }
}