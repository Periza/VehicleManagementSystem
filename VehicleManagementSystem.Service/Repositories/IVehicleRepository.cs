using VehicleManagementSystem.Service.Models;

namespace VehicleManagementSystem.Service.Repositories;

public interface IVehicleRepository
{
    Task<IEnumerable<VehicleMake>> GetAllMakesAsync();

    Task<PaginatedList<VehicleMake>> GetMakesPaginatedAsync(string sortOrder, string searchString, int? pageNumber);

    Task<Optional<VehicleMake>> GetMakeByIdAsync(int id);

    Task AddMakeAsync(VehicleMake make);

    Task UpdateMakeAsync(VehicleMake make);

    Task DeleteMakeAsync(int id);

    Task<IEnumerable<VehicleModel>> GetAllModelsAsync();

    Task<Optional<VehicleModel>> GetModelByIdAsync(int id);

    Task DeleteModelAsync(int id);

    Task<IEnumerable<VehicleModel>> GetModelsByMakeIdAsync(int makeId);

    Task<PaginatedList<VehicleModel>> GetModelsPaginatedAsync(string searchTerm, string sortyBy, int? pageNumber, int pageSize);
}
