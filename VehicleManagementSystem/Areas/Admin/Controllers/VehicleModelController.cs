using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Service.Services.Vehicle;

namespace VehicleManagementSystem.MVC.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class VehicleModelController : Controller
{
    private readonly IVehicleService _vehicleService;

    public VehicleModelController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> Index()
    {
        return View(model: await _vehicleService.GetModelsPaginatedAsync("", "",1, 1));
    }
    
}