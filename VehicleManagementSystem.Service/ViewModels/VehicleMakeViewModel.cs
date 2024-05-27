using VehicleManagementSystem.Service.Models;

namespace VehicleManagementSystem.Service.ViewModels;

public class VehicleMakeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Abrv { get; set; } = string.Empty;
}