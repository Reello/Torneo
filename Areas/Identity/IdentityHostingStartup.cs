using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TorneoJudo.Data;

[assembly: HostingStartup(typeof(TorneoJudo.Areas.Identity.IdentityHostingStartup))]
namespace TorneoJudo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        /*public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TorneoJudoContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TorneoJudoContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                   .AddEntityFrameworkStores<TorneoJudoContext>();
            });
        }*/
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}