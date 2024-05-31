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

    public async Task<IActionResult> Index(string searchTerm, string sortBy, int? pageNumber)
    {
        //SORTING
        // Toggle sort order based on the current state
        string nameSort = sortBy switch
        {
            "name_asc" => "name_desc",
            "name_desc" => "",
            _ => "name_asc"
        };

        string abrvSort = sortBy switch
        {
            "abrv_asc" => "abrv_desc",
            "abrv_desc" => "",
            _ => "abrv_asc"
        };

        string makeSort = sortBy switch
        {
            "make_asc" => "make_desc",
            "make_desc" => "",
            _ => "make_asc"
        };

        ViewBag.NameSortParam = nameSort;
        ViewBag.AbrvSortParam = abrvSort;
        ViewBag.MakeSortParam = makeSort;
        
        return View(model: await _vehicleService.GetModelsPaginatedAsync(searchTerm: searchTerm, sortBy: sortBy, pageNumber: pageNumber));
    }

    public async Task<IActionResult> Upsert(int? id = null) // Update insert
    {
        IEnumerable<VehicleMakeViewModel> avaiableMakes = await _vehicleService.GetAllMakesAsync();
        if (id is null or 0)
        {
            VehicleModelViewModel vmViewModel = new()
            {
                AvaiableMakes = avaiableMakes
            };
            return View(vmViewModel);
        }

        Optional<VehicleModelViewModel> vmOptional = await _vehicleService.GetModelByIdAsync(id: id.Value);
        vmOptional.Value.AvaiableMakes = avaiableMakes;
        if (!vmOptional.HasValue)
            return NotFound();

        return View(vmOptional.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(VehicleModelViewModel vmViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                vmViewModel.AvaiableMakes = await _vehicleService.GetAllMakesAsync();
                return View(model: vmViewModel);
            }
            
            // Check if the selected MakeId exists
            // Could be deleted between get and post
            Optional<VehicleMakeViewModel> makeExists = await _vehicleService.GetMakeByIdAsync(vmViewModel.MakeId);
            if (!makeExists.HasValue)
            {
                ModelState.AddModelError(key: "MakeId", errorMessage: "The selected vehicle make does not exist.");
                vmViewModel.AvaiableMakes = await _vehicleService.GetAllMakesAsync();
                return View(vmViewModel);
            }
            
            if (vmViewModel.Id is 0)
            {
                await _vehicleService.AddModelAsync(vmViewModel: vmViewModel);
                TempData["success"] = "Vehicle Model added successfully!";
            }
            else
            {
                await _vehicleService.AddModelAsync(vmViewModel: vmViewModel);
                TempData["success"] = "Vehicle Model updated successfully!";
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