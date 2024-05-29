using AutoMapper;

namespace VehicleManagementSystem.Service.ValueConverters;

public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
{
    public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
    {
        List<TDestination> items = context.Mapper.Map<List<TDestination>>(source: source.ToList());
        return new PaginatedList<TDestination>(items, source.Count, source.PageIndex, pageSize: source.PageIndex)
        {
            HasNextPage = source.HasNextPage,
            HasPreviousPage = source.HasPreviousPage
        };
    }
}