using IdentityModel;
using Mange.Services.Identity.Data;
using Mange.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace Mange.Services.Identity
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var adminUser = userMgr.FindByNameAsync("admin").Result;

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "dev@smardomi.ir",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(adminUser, "123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(adminUser, new Claim[]{
                    new Claim(JwtClaimTypes.Name, "Saeed Mardomi"),
                    new Claim(JwtClaimTypes.GivenName, "Saeed"),
                    new Claim(JwtClaimTypes.FamilyName, "Mardomi"),
                    new Claim(JwtClaimTypes.WebSite, "https://smardomi.ir"),
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("admin created");

                //============================================================================
                var user = new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@email.com",
                    EmailConfirmed = true
                };
                result = userMgr.CreateAsync(user, "123").GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(user, new Claim[]{
                    new Claim(JwtClaimTypes.Name, "Test User"),
                    new Claim(JwtClaimTypes.GivenName, "Test"),
                    new Claim(JwtClaimTypes.FamilyName, "User"),
                    new Claim(JwtClaimTypes.WebSite, "http://test.com"),
                    new Claim("location", "somewhere")
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("user created");


                var roleCreatedResult = roleMgr.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                if (!roleCreatedResult.Succeeded)
                {
                    throw new Exception(roleCreatedResult.Errors.First().Description);
                }

                roleMgr.CreateAsync(new IdentityRole("User"));

                var addToRoleResult =  userMgr.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception(addToRoleResult.Errors.First().Description);
                }
                userMgr.AddToRoleAsync(user, "User").GetAwaiter().GetResult();

            }
            else
            {
                Log.Debug("admin already exists");
            }
        }
    }
}