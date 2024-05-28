using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Service.Services.Vehicle;
using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.MVC.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class VehicleMakeController : Controller
{
    private readonly IVehicleService _vehicleService;

    public VehicleMakeController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<VehicleMakeViewModel> models = await _vehicleService.GetAllMakesAsync();
        return View(models);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _vehicleService.DeleteMakeAsync(id: id);
            TempData["success"] = "Vehicle Make deleted successfully";
        }
        catch
        {
            TempData["error"] = "Error while deleting vehicle make";
        }

        return RedirectToAction(nameof(Index));
    }
    
}