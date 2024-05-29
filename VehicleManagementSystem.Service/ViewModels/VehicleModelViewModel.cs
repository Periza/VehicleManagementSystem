using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VehicleManagementSystem.Service.Models;

namespace VehicleManagementSystem.Service.ViewModels;

public class VehicleModelViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please select a make.")]
    public int MakeId { get; set; }
    
    [Required]
    [StringLength(maximumLength: 100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(maximumLength: 10, ErrorMessage = "Abbreviation cannot be longer than 10 characters")]
    public string Abrv { get; set; } = string.Empty;
    [ValidateNever]
    public VehicleMake Make { get; set; }
    
    // list of makes for the dropdown
    [ValidateNever]
    public IEnumerable<VehicleMakeViewModel> AvaiableMakes { get; set; }
}