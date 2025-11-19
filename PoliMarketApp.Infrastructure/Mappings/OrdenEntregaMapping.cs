using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Mappings;

public class OrdenEntregaMapping : Profile
{
    public OrdenEntregaMapping()
    {
        CreateMap<OrdenEntrega, OrdenEntregaDto>()
            .ForMember(dest => dest.ClienteNombre, 
                opt => opt.MapFrom(src => src.PedidoVenta != null && src.PedidoVenta.Cliente != null 
                    ? src.PedidoVenta.Cliente.Nombre 
                    : null))
            .ForMember(dest => dest.FechaCreacion, 
                opt => opt.MapFrom(src => src.FechaProgramada))
            .ForMember(dest => dest.FechaEntrega, 
                opt => opt.MapFrom(src => src.FechaEntregaReal))
            .ForMember(dest => dest.EstadoDescripcion, 
                opt => opt.MapFrom(src => src.EstadoOrdenEntrega != null ? src.EstadoOrdenEntrega.Descripcion : null))
            .ForMember(dest => dest.Observaciones, opt => opt.Ignore());
    }
}