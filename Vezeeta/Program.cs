using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Stripe;
using NuGet.Protocol.Core.Types;
using Vezeeta.RepoServices;

namespace Vezeeta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            builder.Services.AddScoped<IDoctorRepository, DoctorRepoService>();
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&(Identity)&&&&&&&&&&&&&&&&&&&&&&&&&&
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<AppUser, IdentityRole, ApplicationDbContext>>()
                .AddRoleStore<RoleStore<IdentityRole, ApplicationDbContext>>();
            builder.Services.AddControllersWithViews();
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&(External login with google)&&&&&&&&&&&&&&&&&&&&&&&&&&
            builder.Services.AddAuthentication().AddGoogle(
                op =>
                {
                    op.ClientId = "330481554308-l3u9f8ggm7esmgmdul0ak3mf84toai3r.apps.googleusercontent.com";
                    op.ClientSecret = "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 ";
                }
            );
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&(Payment)&&&&&&&&&&&&&&&&&&&&&&&&&&
            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
