using AutoMapper;
using VehicleManagementSystem.Service.Models;
using VehicleManagementSystem.Service.ValueConverters;
using VehicleManagementSystem.Service.ViewModels;

namespace VehicleManagementSystem.Service.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
        CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();

        CreateMap(sourceType: typeof(PaginatedList<VehicleMake>), destinationType: typeof(PaginatedList<VehicleMakeViewModel>)).ConvertUsing(typeof(PaginatedListConverter<VehicleMake,VehicleMakeViewModel>));
        
        

    }
}