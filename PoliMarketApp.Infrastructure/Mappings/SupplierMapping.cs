using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Mappings;

public class SupplierMapping : Profile
{
    public SupplierMapping()
    {
        CreateMap<Proveedor, ProveedorDto>();
        CreateMap<CreateProveedorDto, Proveedor>();

        CreateMap<CompraProveedor, CompraProveedorDto>()
            .ForMember(dest => dest.ProveedorNombre, opt => opt.MapFrom(src => src.Proveedor.Nombre))
            .ForMember(dest => dest.EstadoDescripcion, opt => opt.MapFrom(src => src.EstadoCompraProveedor.Descripcion))
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.DetalleComprasProveedores));

        CreateMap<DetalleCompraProveedor, DetalleCompraProveedorDto>()
            .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Producto.Nombre));

        CreateMap<CreateVendedorDto, Vendedor>();
    }
}