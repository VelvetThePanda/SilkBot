using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Silk.Dashboard.Services
{
    public class DiscordRestClientService
    {
        public DiscordRestClient RestClient { get; }

        private readonly IReadOnlyList<DiscordGuild> _guilds;

        public DiscordRestClientService(IHttpContextAccessor accessor)
        {
            var token = accessor.HttpContext!
                .GetTokenAsync(DiscordAuthenticationDefaults.AuthenticationScheme, "access_token")
                .GetAwaiter()
                .GetResult();
            
            var config = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bearer
            };
                
            RestClient = new DiscordRestClient(config);
            RestClient.InitializeAsync().GetAwaiter().GetResult();

            _guilds = RestClient.GetCurrentUserGuildsAsync(100, 0, 0).GetAwaiter().GetResult();
        }

        public async Task<IReadOnlyList<DiscordGuild>> GetAllGuildsAsync()
            => await RestClient.GetCurrentUserGuildsAsync(100, 0, 0);

        public IReadOnlyList<DiscordGuild> GetGuildsByPermission(Permissions perms)
            => _guilds.Where(g => (g.Permissions & perms) != 0).ToList();
    }
}