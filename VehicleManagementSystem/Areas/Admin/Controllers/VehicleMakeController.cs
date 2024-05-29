using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Service;
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

    public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
    {
        //SORTING
        // Toggle sort order based on the current state
        string nameSort = sortOrder switch
        {
            "name_asc" => "name_desc",
            "name_desc" => "",
            _ => "name_asc"
        };

        string abrvSort = sortOrder switch
        {
            "abrv_asc" => "abrv_desc",
            "abrv_desc" => "",
            _ => "abrv_asc"
        };
        
        ViewData["CurrentSort"] = sortOrder;

        ViewData["NameSortParam"] = nameSort;
        ViewData["AbrvSortParam"] = abrvSort;
        
        // FILTERING
        ViewData["CurrentFilter"] = searchString;
        
        PaginatedList<VehicleMakeViewModel> models = await _vehicleService.GetMakesPaginatedAsync(sortOrder: sortOrder, searchString: searchString, pageNumber: pageNumber);
        return View(models);
    }
    
    public async Task<IActionResult> Upsert(int? id = null) // Update insert
    {
        // If id is null or zero create a new VehicleMake view model and pass it to the view
        if (id is null or 0)
        {
            VehicleMakeViewModel vmViewModel = new();
            return View(vmViewModel);
        }

        Optional<VehicleMakeViewModel> vmOptional = await _vehicleService.GetMakeByIdAsync(id.Value);
        if (!vmOptional.HasValue)
            return NotFound();

        return View(vmOptional.Value);
    }
    
    [HttpPost]
    public async Task<IActionResult> Upsert(VehicleMakeViewModel vmViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (vmViewModel.Id is 0)
                {
                    TempData["success"] = "Vehicle Make added successfully!";
                    await _vehicleService.AddMakeAsync(vmViewModel);
                }
                else
                {
                    TempData["success"] = "Vehicle Make updated successfully!";
                    await _vehicleService.UpdateMakeAsync(vmViewModel);
                }
            }
        }
        catch
        {
            TempData["error"] = $"Error while {(vmViewModel.Id is 0 ? "creating" : "updating")} vehicle make";
        }

        return RedirectToAction(actionName: nameof(Index));
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

        return RedirectToAction(actionName: nameof(Index));
    }
    
}