using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Service.Services.Vehicle;


public interface IVehicleService
{
    Task<IEnumerable<VehicleMakeViewModel>> GetAllMakesAsync();
    Task<IEnumerable<VehicleMakeViewModel>> GetMakesPaginatedAsync(string sortOrder, string searchString);
    Task<Optional<VehicleMakeViewModel>> GetMakeByIdAsync(int id);
    Task AddMakeAsync(VehicleMakeViewModel makeViewModel);
    Task UpdateMakeAsync(VehicleMakeViewModel makeViewModel);
    Task DeleteMakeAsync(int id);
    Task<IEnumerable<VehicleModelViewModel>> GetAllModelsAsync();
    Task<Optional<VehicleModelViewModel>> GetModelByIdAsync(int id);
    Task<IEnumerable<VehicleModelViewModel>> GetModelsByMakeIdAsync(int makeId);
    Task<IEnumerable<VehicleModelViewModel>> GetModelsAsync(string searchTerm, string sortBy, bool ascending, int pageNumber, int pageSize);
}
