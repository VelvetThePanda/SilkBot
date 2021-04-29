using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Silk.Dashboard.Services
{
    public class DiscordRestClientService : IDisposable
    {
        private bool _disposed;
        public DiscordRestClient RestClient { get; }

        public DiscordRestClientService(IDashboardTokenStorageService tokenStorageService)
        {
            RestClient = new DiscordRestClient(new DiscordConfiguration
            {
                Token = tokenStorageService.GetToken()?.AccessToken,
                TokenType = TokenType.Bearer
            });
            RestClient.InitializeAsync().GetAwaiter().GetResult();
        }

        public async Task<IReadOnlyList<DiscordGuild>> GetAllGuildsAsync()
            => await RestClient.GetCurrentUserGuildsAsync(100, 0, 0);

        public async Task<IReadOnlyList<DiscordGuild>> GetGuildsByPermissionAsync(Permissions perms)
            => FilterGuildsByPermission(await GetAllGuildsAsync(), perms);

        public IReadOnlyList<DiscordGuild> FilterGuildsByPermission(IReadOnlyList<DiscordGuild> guilds,
            Permissions perms)
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