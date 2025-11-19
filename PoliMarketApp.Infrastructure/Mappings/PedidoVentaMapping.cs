using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Mappings;

public class PedidoVentaMapping : Profile
{
    public PedidoVentaMapping()
    {
        CreateMap<PedidoVenta, PedidoVentaDto>()
            .ForMember(dest => dest.VendedorNombre, 
                opt => opt.MapFrom(src => src.Vendedor != null ? $"{src.Vendedor.Nombre} {src.Vendedor.Apellido}" : null))
            .ForMember(dest => dest.ClienteNombre, 
                opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nombre : null))
            .ForMember(dest => dest.FechaPedido, 
                opt => opt.MapFrom(src => src.FechaCreacion))
            .ForMember(dest => dest.EstadoDescripcion, 
                opt => opt.MapFrom(src => src.EstadoPedidoVenta != null ? src.EstadoPedidoVenta.Descripcion : null));

        CreateMap<DetallePedidoVenta, DetallePedidoVentaDto>();

        CreateMap<CreatePedidoVentaDto, PedidoVenta>()
            .ForMember(dest => dest.PedidoVentaId, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.EstadoPedidoVentaId, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Total, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.Vendedor, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoPedidoVenta, opt => opt.Ignore())
            .ForMember(dest => dest.OrdenesEntregas, opt => opt.Ignore())
            .ForMember(dest => dest.DetallePedidosVenta, opt => opt.MapFrom(src => src.Detalles));

        CreateMap<DetallePedidoVentaDto, DetallePedidoVenta>()
            .ForMember(dest => dest.DetallePedidoVentaId, opt => opt.Ignore())
            .ForMember(dest => dest.PedidoVentaId, opt => opt.Ignore())
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Cantidad * src.PrecioUnitario))
            .ForMember(dest => dest.PedidoVenta, opt => opt.Ignore())
            .ForMember(dest => dest.Producto, opt => opt.Ignore());
    }
}