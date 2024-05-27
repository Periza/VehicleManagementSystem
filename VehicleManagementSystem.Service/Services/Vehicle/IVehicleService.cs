using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Service.Services.Vehicle;


public interface IVehicleService
{
    // Methods for Vehicle Make
    Task<IEnumerable<VehicleMakeViewModel>> GetAllMakesAsync();
    Task<VehicleMakeViewModel> GetMakeByIdAsync(int id);
    Task AddMakeAsync(VehicleMakeViewModel makeViewModel);
    Task UpdateMakeAsync(VehicleMakeViewModel makeViewModel);
    Task DeleteMakeAsync(int id);

    // Methods for Vehicle Model
    Task<IEnumerable<VehicleModelViewModel>> GetAllModelsAsync();
    Task<IEnumerable<VehicleModelViewModel>> GetModelsByMakeIdAsync(int makeId);

    // Sorting Method
    IEnumerable<VehicleModelViewModel> SortModels(IEnumerable<VehicleModelViewModel> models, string sortBy, bool ascending);

    // Filtering Method
    IEnumerable<VehicleModelViewModel> FilterModels(IEnumerable<VehicleModelViewModel> models, string searchTerm);

    // Paging Method
    IEnumerable<VehicleModelViewModel> GetModelsForPage(IEnumerable<VehicleModelViewModel> models, int pageNumber, int pageSize);
}

