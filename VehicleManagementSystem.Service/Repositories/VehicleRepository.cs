using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using VehicleManagementSystem.Service.Models;

namespace VehicleManagementSystem.Service.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VehicleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<VehicleMake>> GetAllMakesAsync()
    {
        return await _dbContext.VehicleMakes.ToListAsync();
    }

    public async Task<PaginatedList<VehicleMake>> GetMakesPaginatedAsync(string sortOrder, string searchString, int? pageNumber)
    {
        IQueryable<VehicleMake> makes = _dbContext.VehicleMakes.AsNoTracking().AsQueryable();

        makes = sortOrder switch
        {
            "name_asc" => makes.OrderBy(vm => vm.Name),
            "name_desc" => makes.OrderByDescending(vm => vm.Name),
            "abrv_asc" => makes.OrderBy(vm => vm.Abrv),
            "abrv_desc" => makes.OrderByDescending(vm => vm.Abrv),
            _ => makes
        };

        if (!string.IsNullOrEmpty(searchString))
        {
            makes = makes.Where(make => make.Name.Contains(searchString) || make.Abrv.Contains(searchString));
        }
        
        int pageSize = 5;


        return await PaginatedList<VehicleMake>.CreateAsync(source: makes, pageIndex: pageNumber ?? 1,
            pageSize: pageSize);
    }

    public async Task<Optional<VehicleMake>> GetMakeByIdAsync(int id)
    {
        VehicleMake? make = await _dbContext.VehicleMakes.FindAsync(keyValues: id);
        return make is not null ? Optional<VehicleMake>.Of(value: make) : Optional<VehicleMake>.Empty();
    }

    public async Task AddMakeAsync(VehicleMake make)
    {
        await _dbContext.VehicleMakes.AddAsync(entity: make);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMakeAsync(VehicleMake make)
    {
        _dbContext.VehicleMakes.Update(entity: make);
        await _dbContext.SaveChangesAsync();
    }
    

    public async Task DeleteMakeAsync(int id)
    {
        VehicleMake? make = await _dbContext.VehicleMakes.FindAsync(keyValues: id);
        if (make is not null)
        {
            _dbContext.VehicleMakes.Remove(entity: make);
            await _dbContext.SaveChangesAsync();
        }
    }
    
    // Model methods
    public async Task<IEnumerable<VehicleModel>> GetAllModelsAsync()
    {
        return await _dbContext.VehicleModels.Include(vm => vm.Make).ToListAsync();
    }

    public async Task<Optional<VehicleModel>> GetModelByIdAsync(int id)
    {
        VehicleModel? model = await _dbContext.VehicleModels.FindAsync(id);
        return model is not null ? Optional<VehicleModel>.Of(model) : Optional<VehicleModel>.Empty();
    }

    public async Task AddModelAsync(VehicleModel vehicleModel)
    {
        await _dbContext.VehicleModels.AddAsync(entity: vehicleModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateModelAsync(VehicleModel vehicleModel)
    {
        _dbContext.VehicleModels.Update(entity: vehicleModel);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteModelAsync(int id)
    {
        VehicleModel? model = await _dbContext.VehicleModels.FindAsync(id);
        if (model is not null)
        {
            _dbContext.VehicleModels.Remove(entity: model);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<VehicleModel>> GetModelsByMakeIdAsync(int makeId)
    {
        return await _dbContext.VehicleModels.Where(m => m.MakeId == makeId).ToListAsync();
    }

    public async Task<PaginatedList<VehicleModel>> GetModelsPaginatedAsync(string searchTerm, string sortBy, int? pageNumber)
    {
        IQueryable<VehicleModel> models = _dbContext.VehicleModels.Include(navigationPropertyPath: vm => vm.Make).AsQueryable();

        models = sortBy switch
        {
            "name_asc" => models.OrderBy(vm => vm.Name),
            "name_desc" => models.OrderByDescending(vm => vm.Name),
            "abrv_asc" => models.OrderBy(vm => vm.Abrv),
            "abrv_desc" => models.OrderByDescending(vm => vm.Abrv),
            "make_asc" => models.OrderBy(vm => vm.Make.Name),
            "make_desc" => models.OrderByDescending(vm => vm.Make.Name),
            _ => models
        };

        if (!string.IsNullOrEmpty(searchTerm))
        {
            models = models.Where(model => model.Name.Contains(searchTerm) || model.Abrv.Contains(searchTerm) || model.Make.Name.Contains(searchTerm));
        }

        int pageSize = 5;
        
        return await PaginatedList<VehicleModel>.CreateAsync(source: models, pageIndex: pageNumber ?? 1,
            pageSize: pageSize);
    }
}