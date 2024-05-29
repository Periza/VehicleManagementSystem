using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Service.Services.Vehicle;


public interface IVehicleService
{
    Task<IEnumerable<VehicleMakeViewModel>> GetAllMakesAsync();
    Task<PaginatedList<VehicleMakeViewModel>> GetMakesPaginatedAsync(string sortOrder, string searchString, int? pageNumber);
    Task<Optional<VehicleMakeViewModel>> GetMakeByIdAsync(int id);
    Task AddMakeAsync(VehicleMakeViewModel makeViewModel);
    Task UpdateMakeAsync(VehicleMakeViewModel makeViewModel);
    Task DeleteMakeAsync(int id);
    Task<IEnumerable<VehicleModelViewModel>> GetAllModelsAsync();
    Task<Optional<VehicleModelViewModel>> GetModelByIdAsync(int id);

    Task AddModelAsync(VehicleModelViewModel vmViewModel);

    Task UpdateModelAsync(VehicleModelViewModel vmViewModel);
    Task DeleteModelAsync(int id);
    Task<IEnumerable<VehicleModelViewModel>> GetModelsByMakeIdAsync(int makeId);
    Task<PaginatedList<VehicleModelViewModel>> GetModelsPaginatedAsync(string searchTerm, string sortBy, int pageNumber, int pageSize);
}
