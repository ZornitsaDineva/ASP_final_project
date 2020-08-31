using System;
using ContosoUniversity.Areas.Identity.Data;
using ContosoUniversity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



[assembly: HostingStartup(typeof(ContosoUniversity.Areas.Identity.IdentityHostingStartup))]
namespace ContosoUniversity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {


        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ContosoUniversityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ContosoUniversityContextConnection")));

                services.AddDefaultIdentity<ContosoUniversityUser>()
                    .AddEntityFrameworkStores<ContosoUniversityContext>();

                services.AddAuthentication()
                    .AddMicrosoftAccount(microsoftOptions =>
                    {
                        microsoftOptions.ClientId = context.Configuration["Authentication:Microsoft:ClientId"];
                        microsoftOptions.ClientSecret = context.Configuration["Authentication:Microsoft:ClientSecret"];
                    })
                    .AddGoogle (o =>
                    { 
                        o.ClientId = context.Configuration["Authentication1:Google:ClientId"];
                        o.ClientSecret = context.Configuration["Authentication1:Google:ClientSecret"];

                    })
                    
                /*.AddTwitter(twitterOptions => { })*/
                    .AddFacebook(facebookOptions =>
                    {
                        facebookOptions.AppId = context.Configuration["Authentication2:facebook:appid"];
                        facebookOptions.AppSecret = context.Configuration["Authentication2:facebook:appsecret"];
                    });
            });
        }
       
    }
}