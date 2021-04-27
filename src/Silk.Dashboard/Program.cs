using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Silk.Dashboard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /* Todo: Fix Migrations (need to start Silk.Core.Logic currently if Postgre database for Silk has not been created) */
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}