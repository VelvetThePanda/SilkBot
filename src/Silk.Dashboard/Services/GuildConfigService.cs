using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Silk.Data;
using Silk.Data.Models;

namespace Silk.Dashboard.Services
{
    public class GuildConfigService
    {
        public SilkDbContext Database { get; set; }

        public GuildConfigService(SilkDbContext context) => Database = context;

        public async Task<GuildConfig> GetConfig(ulong guildId)
            => await Database.GuildConfigs.FirstOrDefaultAsync(gc => gc.GuildId == guildId);

        public async Task<bool> ChangeConfig(ulong guildId, GuildConfig newConfig)
        {
            var config = await GetConfig(guildId);

            config = newConfig;
            try
            {
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}