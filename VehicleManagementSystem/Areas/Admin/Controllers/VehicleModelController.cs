using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Service;
using VehicleManagementSystem.Service.Services.Vehicle;
using VehicleManagementSystem.Service.ViewModels;

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
        return View(model: await _vehicleService.GetModelsPaginatedAsync("", "",1, 5));
    }

    public async Task<IActionResult> Upsert(int? id = null) // Update insert
    {
        if (id is null or 0)
        {
            VehicleModelViewModel vmViewModel = new();
            return View(vmViewModel);
        }

        Optional<VehicleModelViewModel> vmOptional = await _vehicleService.GetModelByIdAsync(id: id.Value);
        if (!vmOptional.HasValue)
            return NotFound();

        return View(vmOptional.Value);
    }

    public async Task<IActionResult> Upsert(VehicleModelViewModel vmViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (vmViewModel.Id is 0)
                {
                    await _vehicleService.AddModelAsync(vmViewModel: vmViewModel);
                    TempData["success"] = "Vehicle Model added successfully!";
                }
                else
                {
                    await _vehicleService.AddModelAsync(vmViewModel: vmViewModel);
                    TempData["success"] = "Vehicle Model update successfully!";
                }
            }
        }
        catch
        {
            TempData["error"] = $"Error while {(vmViewModel.Id is 0 ? "creating" : "updating")} vehicle model";
        }
        
        return RedirectToAction(actionName: nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _vehicleService.DeleteModelAsync(id: id);
            TempData["success"] = "Vehicle Model deleted successfully";
        }
        catch
        {
            TempData["error"] = "Error while deleting vehicle make";
        }

        return RedirectToAction(actionName: nameof(Index));
    }
    
}