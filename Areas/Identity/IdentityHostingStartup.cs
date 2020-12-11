using System;
using Asernatat_3.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Asernatat_3.Areas.Identity.IdentityHostingStartup))]
namespace Asernatat_3.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDataContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("AuthDataContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                     .AddRoles<IdentityRole>()
                     .AddEntityFrameworkStores<AuthDataContext>();
            });
        }
    }
}