using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Service.Models;


namespace Project.Service.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<VehicleMake> VehicleMakes { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed VehicleMake data
        modelBuilder.Entity<VehicleMake>().HasData(
            new VehicleMake { Id = 1, Name = "BMW", Abrv = "BMW" },
            new VehicleMake { Id = 2, Name = "Ford", Abrv = "FORD" },
            new VehicleMake { Id = 3, Name = "Volkswagen", Abrv = "VW" }
        );
        
        // Seed VehicleModel data
        modelBuilder.Entity<VehicleModel>().HasData(
            new VehicleModel { Id = 1, MakeId = 1, Name = "128", Abrv = "128" },
            new VehicleModel { Id = 2, MakeId = 1, Name = "325", Abrv = "325" },
            new VehicleModel { Id = 3, MakeId = 1, Name = "X5", Abrv = "X5" },
            new VehicleModel { Id = 4, MakeId = 2, Name = "Fiesta", Abrv = "FIE" },
            new VehicleModel { Id = 5, MakeId = 2, Name = "Focus", Abrv = "FOC" },
            new VehicleModel { Id = 6, MakeId = 3, Name = "Golf", Abrv = "GOL" },
            new VehicleModel { Id = 7, MakeId = 3, Name = "Passat", Abrv = "PAS" }
        );
        
    }
}