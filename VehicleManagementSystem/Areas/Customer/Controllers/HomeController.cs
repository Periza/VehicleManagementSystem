using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleManagementSystem.Models;
using VehicleManagementSystem.Service.Services.Vehicle;
using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Controllers
{
    [Area(areaName: "Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;

        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Vehicles()
        {
            IEnumerable<VehicleModelViewModel> vehicles = await _vehicleService.GetAllModelsAsync();
            return View(vehicles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
