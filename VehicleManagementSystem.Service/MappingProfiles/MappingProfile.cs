using AutoMapper;
using VehicleManagementSystem.Service.Models;
using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Service.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
        CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
    }
}