using AutoMapper;
using VehicleManagementSystem.Service.Models;
using VehicleManagementSystem.Service.Repositories;
using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Service.Services.Vehicle;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;

    public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<VehicleMakeViewModel>> GetAllMakesAsync()
    {
        IEnumerable<VehicleMake> makes = await _vehicleRepository.GetAllMakesAsync();
        return _mapper.Map<IEnumerable<VehicleMakeViewModel>>(source: makes);
    }

    public async Task<PaginatedList<VehicleMakeViewModel>> GetMakesPaginatedAsync(string sortOrder, string searchString, int? pageNumber)
    {
        PaginatedList<VehicleMake> makes = await _vehicleRepository.GetMakesPaginatedAsync(sortOrder: sortOrder, searchString: searchString, pageNumber: pageNumber);
        PaginatedList<VehicleMakeViewModel> paginatedMakes = _mapper.Map<PaginatedList<VehicleMakeViewModel>>(source: makes);
        return paginatedMakes;
    }

    public async Task<Optional<VehicleMakeViewModel>> GetMakeByIdAsync(int id)
    {
        Optional<VehicleMake> makeOptional = await _vehicleRepository.GetMakeByIdAsync(id: id);
        return makeOptional.HasValue ? Optional<VehicleMakeViewModel>.Of(_mapper.Map<VehicleMakeViewModel>(makeOptional.Value)) : Optional<VehicleMakeViewModel>.Empty();
    }

    public async Task AddMakeAsync(VehicleMakeViewModel makeViewModel)
    {
        VehicleMake make = _mapper.Map<VehicleMake>(source: makeViewModel);
        await _vehicleRepository.AddMakeAsync(make: make);
    }
    
    public async Task UpdateMakeAsync(VehicleMakeViewModel makeViewModel)
    {
        VehicleMake make = _mapper.Map<VehicleMake>(makeViewModel);
        await _vehicleRepository.UpdateMakeAsync(make: make);
    }

    public async Task DeleteMakeAsync(int id)
    {
        await _vehicleRepository.DeleteMakeAsync(id: id);
    }
    
    // Model methods
    public async Task<IEnumerable<VehicleModelViewModel>> GetAllModelsAsync()
    {
        IEnumerable<VehicleModel> models = await _vehicleRepository.GetAllModelsAsync();
        return _mapper.Map<IEnumerable<VehicleModelViewModel>>(models);
    }

    public async Task<Optional<VehicleModelViewModel>> GetModelByIdAsync(int id)
    {
        Optional<VehicleModel> modelOptional = await _vehicleRepository.GetModelByIdAsync(id: id);
        return modelOptional.HasValue ? Optional<VehicleModelViewModel>.Of(_mapper.Map<VehicleModelViewModel>(modelOptional.Value)) : Optional<VehicleModelViewModel>.Empty();

    }

    public async Task AddModelAsync(VehicleModelViewModel vmViewModel)
    {
        VehicleModel model = _mapper.Map<VehicleModel>(vmViewModel);
        await _vehicleRepository.AddModelAsync(vehicleModel: model);
    }

    public async Task UpdateModelAsync(VehicleModelViewModel vmViewModel)
    {
        VehicleModel model = _mapper.Map<VehicleModel>(vmViewModel);
        await _vehicleRepository.AddModelAsync(vehicleModel: model);
    }
    public async Task DeleteModelAsync(int id)
    {
        await _vehicleRepository.DeleteModelAsync(id: id);
    }

    public async Task<IEnumerable<VehicleModelViewModel>> GetModelsByMakeIdAsync(int makeId)
    {
        IEnumerable<VehicleModel> models = await _vehicleRepository.GetModelsByMakeIdAsync(makeId: makeId);
        return _mapper.Map<IEnumerable<VehicleModelViewModel>>(models);
    }

    public async Task<PaginatedList<VehicleModelViewModel>> GetModelsPaginatedAsync(string searchTerm, string sortBy, int pageNumber, int pageSize)
    {
        PaginatedList<VehicleModel> models = await _vehicleRepository.GetModelsPaginatedAsync(searchTerm: searchTerm, sortyBy: sortBy, pageNumber: pageNumber, pageSize: pageSize);
        return _mapper.Map<PaginatedList<VehicleModelViewModel>>(models);
    }
}