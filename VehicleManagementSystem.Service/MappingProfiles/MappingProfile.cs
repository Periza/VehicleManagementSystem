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

        CreateMap(sourceType: typeof(PaginatedList<VehicleMake>), destinationType: typeof(PaginatedList<VehicleMakeViewModel>)).ConvertUsing(typeConverterType: typeof(PaginatedListConverter<VehicleMake,VehicleMakeViewModel>));
        CreateMap(sourceType: typeof(PaginatedList<VehicleModel>), destinationType: typeof(PaginatedList<VehicleModelViewModel>)).ConvertUsing(typeConverterType: typeof(PaginatedListConverter<VehicleModel, VehicleModelViewModel>));
    }
}