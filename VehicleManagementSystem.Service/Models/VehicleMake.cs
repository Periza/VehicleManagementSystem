using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.Service.Models;

public record VehicleMake
{
    [Key] 
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Abrv { get; set; } = string.Empty;
}