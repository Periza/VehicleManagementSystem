using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace VehicleManagementSystem.Service.Models;

public record VehicleModel
{
    [Key] 
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Abrv { get; set; } = string.Empty;
    
    public int MakeId { get; set; }
    
    [ForeignKey(name: "MakeId")]
    public VehicleMake Make { get; set; }
}