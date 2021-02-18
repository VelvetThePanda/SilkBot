using AspNet.Security.OAuth.Discord;
using DSharpPlus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Silk.Dashboard.Services.Concretions
{
    public class DiscordRestClientService
    {
        public DiscordRestClient Client { get; set; }

        public DiscordRestClientService(string token)
        {
            Client = new DiscordRestClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bearer
            });
        }
    }
}
