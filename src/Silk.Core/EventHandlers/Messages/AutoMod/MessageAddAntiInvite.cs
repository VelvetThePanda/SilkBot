﻿using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using MediatR;
using Silk.Core.Data.Models;
using Silk.Core.Services;
using Silk.Core.Services.Interfaces;

namespace Silk.Core.EventHandlers.Messages.AutoMod
{
    public class MessageAddAntiInvite
    {
        private readonly ConfigService _config;
        private readonly IInfractionService _infractionService;
        private readonly IMediator _mediator;

        public MessageAddAntiInvite(IInfractionService infractionService, IMediator mediator, ConfigService config)
        {
            _infractionService = infractionService;
            _mediator = mediator;
            _config = config;
        }

        public async Task CheckForInvite(DiscordClient client, MessageCreateEventArgs args)
        {
            if (!args.Channel.IsPrivate)
            {
                GuildConfig config = await _config.GetConfigAsync(args.Guild.Id);

                bool hasInvite = AntiInviteCore.CheckForInvite(client, args.Message, config, out string invite);
                bool isBlacklisted = await AntiInviteCore.IsBlacklistedInvite(client, args.Message, config, invite!);

                if (hasInvite && isBlacklisted)
                    await AntiInviteCore.TryAddInviteInfractionAsync(config, args.Message, _infractionService);
            }
        }
    }
}