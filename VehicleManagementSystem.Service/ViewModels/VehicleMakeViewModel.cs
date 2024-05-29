using System.ComponentModel.DataAnnotations;
using VehicleManagementSystem.Service.Models;

namespace VehicleManagementSystem.Service.ViewModels;

public class VehicleMakeViewModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(maximumLength: 100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(maximumLength: 10, ErrorMessage = "Abbreviation cannot be longer than 10 characters")]
    public string Abrv { get; set; } = string.Empty;
    
}