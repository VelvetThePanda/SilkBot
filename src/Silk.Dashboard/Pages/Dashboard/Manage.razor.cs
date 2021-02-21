using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Components;
using Silk.Dashboard.Extensions;
using Silk.Dashboard.Services;
using Silk.Data;
using Silk.Data.Models;

namespace Silk.Dashboard.Pages.Dashboard
{
    public partial class Manage : ComponentBase
    {
        [Inject] private DiscordRestClientService RestClientService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private SilkDbContext SilkDbContext { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        [Parameter] public string GuildId { get; set; }

        private DiscordGuild _guild;
        private GuildConfig _config;

        protected override async Task OnInitializedAsync()
        {
            _guild = (await RestClientService
                    .GetGuildsByPermissionAsync(Permissions.ManageGuild))
                .FirstOrDefault(g => g.Id == ulong.Parse(GuildId));

            if (_guild is null)
            {
                NavigationManager.NavigateTo("/Dashboard/Profile");
            }

            _config = await SilkDbContext.GuildConfigs.GetConfig(_guild.Id) ?? CreateNewConfig();
        }

        private GuildConfig CreateNewConfig()
        {
            return new()
            {
                GuildId = ulong.Parse(this.GuildId),
            };
        }

        private async Task SaveChangesAsync()
        {
            var guildId = ulong.Parse(this.GuildId);

            _config.GuildId = guildId;

            var updated = await SilkDbContext.ChangeConfig(guildId, _config);
            if (updated)
                ToastService.ShowSuccess("Successfully saved config!");
            else
                ToastService.ShowError("Uh-oh! Something went wrong!");
        }
    }
}