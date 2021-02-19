using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Silk.Data;
using Silk.Data.Models;

namespace Silk.Dashboard.Extensions
{
    public static class GuildConfigService
    {
        public static async Task<GuildConfig> GetConfig(this DbSet<GuildConfig> configs, ulong guildId)
            => await configs.FirstOrDefaultAsync(gc => gc.GuildId == guildId);

        public static async Task<bool> ChangeConfig(this SilkDbContext context, ulong guildId, GuildConfig newConfig)
        {
            // TODO: if the config on the guild doesn't exist, need to create add the config to the guild
            
            Guild? guild = await context.Guilds
                .Include(g => g.Configuration)
                .FirstOrDefaultAsync(g => g.Id == guildId);

            if (guild is null) return false;

            GuildConfig? config = await context.GuildConfigs.GetConfig(guildId);
            
            // TODO: Check this logic?
            if (config is null)
            {
                config = newConfig;
                config.Guild = guild;
                context.GuildConfigs.Attach(config);
            }

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