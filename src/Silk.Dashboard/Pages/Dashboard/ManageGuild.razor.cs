using System;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using DSharpPlus;
using DSharpPlus.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Silk.Core.Data.MediatR.Guilds;
using Silk.Core.Data.Models;
using Silk.Dashboard.Services;
using Silk.Shared.Constants;

namespace Silk.Dashboard.Pages.Dashboard
{
    public partial class ManageGuild : ComponentBase
    {
        [Inject] private DiscordRestClientService RestClientService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IMediator Mediator { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        [Parameter] public string GuildId { get; set; }

        private readonly GreetingOption[] _greetingOptions = Enum.GetValues<GreetingOption>();

        private DiscordGuild _guild;
        private GuildConfig _config;

        protected override async Task OnInitializedAsync()
        {
            _guild = (await RestClientService.GetGuildsByPermissionAsync(Permissions.ManageGuild))
                .FirstOrDefault(g => g.Id == ulong.Parse(GuildId));

            if (_guild is null)
            {
                NavigationManager.NavigateTo("/Dashboard/Profile");
            }
            else
            {
                GuildConfig configResponse = await Mediator.Send<GuildConfig>(new GetGuildConfigRequest(_guild.Id));
                if (configResponse is null)
                {
                    var guild = await GetOrCreateNewGuild();
                    _config = guild.Configuration;
                }
                else
                {
                    _config = configResponse;
                }
            }
        }

        private async Task<Guild> GetOrCreateNewGuild()
        {
            return await Mediator.Send(new GetOrCreateGuildRequest(ulong.Parse(this.GuildId), StringConstants.DefaultCommandPrefix));
        }

        private UpdateGuildConfigRequest CompleteUpdateGuildConfigRequest()
        {
            var guildId = ulong.Parse(this.GuildId);
            _config.GuildId = guildId;

            // TODO: Refactor or use/unify logic from UpdateGuildConfigRequest
            return new UpdateGuildConfigRequest(guildId)
            {
                MuteRoleId = _config.MuteRoleId,
                GreetingOption = _config.GreetingOption,
                LoggingChannel = _config.LoggingChannel,
                GreetingChannelId = _config.GreetingChannel,
                VerificationRoleId = _config.VerificationRole,

                ScanInvites = _config.ScanInvites,
                BlacklistWords = _config.BlacklistWords,
                BlacklistInvites = _config.BlacklistInvites,
                LogMembersJoining = _config.LogMemberJoins,
                UseAggressiveRegex = _config.UseAggressiveRegex,
                WarnOnMatchedInvite = _config.WarnOnMatchedInvite,
                DeleteOnMatchedInvite = _config.DeleteMessageOnMatchedInvite,
                GreetOnVerificationRole = _config.GreetOnVerificationRole,
                GreetOnScreeningComplete = _config.GreetOnVerificationRole,

                MaxUserMentions = _config.MaxUserMentions,
                MaxRoleMentions = _config.MaxRoleMentions,

                GreetingText = _config.GreetingText,

                InfractionSteps = _config.InfractionSteps,
                AllowedInvites = _config.AllowedInvites,
                DisabledCommands = _config.DisabledCommands,
                SelfAssignableRoles = _config.SelfAssignableRoles
            };
        }

        private async Task SaveChangesAsync()
        {
            var updated = await Mediator.Send(CompleteUpdateGuildConfigRequest());
            if (updated is not null)
            {
                ToastService.ShowSuccess("Successfully saved config!");
                _config = updated;
            }
            else
            {
                ToastService.ShowError("Uh-oh! Something went wrong!");
            }
        }
    }
}