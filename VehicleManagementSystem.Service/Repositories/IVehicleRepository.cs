using VehicleManagementSystem.Service.Models;

namespace VehicleManagementSystem.Service.Repositories;

public interface IVehicleRepository
{
    Task<IEnumerable<VehicleMake>> GetAllMakesAsync();

    Task<Optional<VehicleMake>> GetMakeByIdAsync(int id);

    Task AddMakeAsync(VehicleMake make);

    Task UpdateMakeAsync(VehicleMake make);

    Task DeleteMakeAsync(int id);

    Task<IEnumerable<VehicleModel>> GetAllModelsAsync();

    Task<Optional<VehicleModel>> GetModelByIdAsync(int id);

    Task<IEnumerable<VehicleModel>> GetModelsByMakeIdAsync(int makeId);

    Task<IEnumerable<VehicleModel>> GetModelsAsync(string searchTerm, string sortyBy, bool ascending, int pageNumber, int pageSize);
}
