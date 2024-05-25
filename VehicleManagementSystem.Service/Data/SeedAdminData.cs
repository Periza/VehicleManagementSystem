using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Service.Data;

public static class SeedAdminData
{
    public static async Task InitializeAsync(this IServiceProvider serviceProvider)
    {
        // Get userManager and serviceProvider services
        UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        // If admin role doesn't exist create it
        if (!await roleManager.RoleExistsAsync(roleName: "Admin")) ;
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            
        // Check if the admin user already exists
        IdentityUser? adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser is null)
        {
            // If it does not exist, create it
            adminUser = new IdentityUser()
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user: adminUser, password: "Admin@123");
            
            // Assign the admin role to admin user
            await userManager.AddToRoleAsync(user: adminUser, role: "Admin");
        }

    }
}