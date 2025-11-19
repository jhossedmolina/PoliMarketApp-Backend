using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Mappings;

public class ProductoMapping : Profile
{
    public ProductoMapping()
    {
        CreateMap<Producto, ProductoDto>()
            .ForMember(dest => dest.PrecioVenta, opt => opt.MapFrom(src => src.PrecioUnitario))
            .ReverseMap()
            .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioVenta));
    }
}