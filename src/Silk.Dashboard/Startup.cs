using System;
using AspNet.Security.OAuth.Discord;
using Blazored.Toast;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silk.Dashboard.Services;
using Silk.Data;

namespace Silk.Dashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddHttpContextAccessor();
            
            services.AddScoped<DiscordRestClientService>();

            services.AddDbContext<SilkDbContext>(o =>
                o.UseNpgsql(Configuration.GetConnectionString("dbConnection")));

            services.AddMediatR(typeof(SilkDbContext));
            
            services.AddBlazoredToast();

            services.AddAuthentication(opt =>
                {
                    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
                })
                .AddDiscord(opt =>
                {
                    opt.ClientId = Configuration["Discord:AppId"];
                    opt.ClientSecret = Configuration["Discord:AppSecret"];

                    opt.CallbackPath = DiscordAuthenticationDefaults.CallbackPath;

                    opt.Scope.Add("guilds");

                    opt.UsePkce = true;
                    opt.SaveTokens = true;
                })
                .AddCookie();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}