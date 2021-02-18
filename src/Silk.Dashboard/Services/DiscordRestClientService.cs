using System;
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
    public class DiscordRestClientService : IDisposable
    {
        public DiscordRestClient RestClient { get; }

        private bool _disposed;
        private readonly IReadOnlyList<DiscordGuild> _guilds;

        // TODO: (optional) See if there's a way to add auth token to client on successful authentication
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

        public IReadOnlyList<DiscordGuild> GetGuildsByPermission(IReadOnlyList<DiscordGuild> guilds, Permissions perms)
            => guilds.Where(g => (g.Permissions & perms) != 0).ToList();

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                RestClient?.Dispose();
            }

            _disposed = true;
        }
    }
}