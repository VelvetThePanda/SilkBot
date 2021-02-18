using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Silk.Data;
using Silk.Data.Models;

namespace Silk.Dashboard.Services
{
    public static class GuildConfigService
    {
        public static async Task<GuildConfig> GetConfig(this DbSet<GuildConfig> configs, ulong guildId)
            => await configs.FirstOrDefaultAsync(gc => gc.GuildId == guildId);

        public static async Task<bool> ChangeConfig(this SilkDbContext context, ulong guildId, GuildConfig newConfig)
        {
            var config = await context.GuildConfigs.GetConfig(guildId);

            config = newConfig;
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}