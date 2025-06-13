using Microsoft.AspNetCore.Identity;
using TechCorp.Models;

namespace TechCorp.Services;

public class AuthService
{
    private static readonly string defaultEmail = "admin@techcorp.com";
    private static readonly string defaultPassword = "P@ssword1!";
    private static readonly string adminRole = "Admin";
    public static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AdminUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        var defaultUser = await userManager.FindByEmailAsync(defaultEmail);
        if (defaultUser == null)
        {
            defaultUser = new AdminUser
            {
                UserName = defaultEmail,
                Role = "Administrator",
                Email = defaultEmail,
                EmailConfirmed = true,
                FullName = "Admin User",
                Department = "IT",
                CreatedAt = DateTime.Now
            };
            
            await userManager.CreateAsync(defaultUser, defaultPassword);
        }

        if (!await userManager.IsInRoleAsync(defaultUser, adminRole))
        {
            await userManager.AddToRoleAsync(defaultUser, adminRole);
        }
    }
}