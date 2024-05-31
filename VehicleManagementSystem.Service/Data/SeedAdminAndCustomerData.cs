using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleManagementSystem.Service.Models;

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

        // Initialize models and makes

        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Look for any board games.
            if (context.VehicleMakes.Any() || context.VehicleModels.Any())
            {
                return; // Data was already seeded
            }


            // Seed Vehicle data

            context.VehicleMakes.AddRange(
                new VehicleMake { Id = 1, Name = "BMW", Abrv = "BMW" },
                new VehicleMake { Id = 2, Name = "Ford", Abrv = "FORD" },
                new VehicleMake { Id = 3, Name = "Volkswagen", Abrv = "VW" }
            );

            context.VehicleModels.AddRange(
                new VehicleModel { Id = 1, MakeId = 1, Name = "128", Abrv = "128" },
                new VehicleModel { Id = 2, MakeId = 1, Name = "325", Abrv = "325" },
                new VehicleModel { Id = 3, MakeId = 1, Name = "X5", Abrv = "X5" },
                new VehicleModel { Id = 4, MakeId = 2, Name = "Fiesta", Abrv = "FIE" },
                new VehicleModel { Id = 5, MakeId = 2, Name = "Focus", Abrv = "FOC" },
                new VehicleModel { Id = 6, MakeId = 3, Name = "Golf", Abrv = "GOL" },
                new VehicleModel { Id = 7, MakeId = 3, Name = "Passat", Abrv = "PAS" }
            );

            context.SaveChanges();
        }
    }
}
