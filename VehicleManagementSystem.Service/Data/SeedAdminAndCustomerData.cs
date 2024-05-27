using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Service.Data;

public static class SeedAdminAndCusomerData
{
    public static async Task InitializeAsync(this IServiceProvider serviceProvider)
    {
        // Get userManager and serviceProvider services
        UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        // If admin role doesn't exist create it
        if (!await roleManager.RoleExistsAsync(roleName: "Admin"))
            await roleManager.CreateAsync(role: new IdentityRole("Admin"));
            
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
        
        // If customer role doesn't exist create it
        if (!await roleManager.RoleExistsAsync(roleName: "Customer"))
            await roleManager.CreateAsync(role: new IdentityRole("Customer"));

        IdentityUser? customerUser = await userManager.FindByEmailAsync("customer@example.com");
        if (customerUser is null)
        {
            // If it does not exist, create it
            customerUser = new IdentityUser()
            {
                UserName = "customer@example.com",
                Email = "customer@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user: customerUser, password: "Customer@123");
            
            // Assign the customer role to customer user
            await userManager.AddToRoleAsync(user: customerUser, role: "Customer");
        }

    }
}