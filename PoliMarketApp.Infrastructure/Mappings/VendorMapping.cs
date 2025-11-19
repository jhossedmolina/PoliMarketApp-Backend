using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Mappings
{
    public class VendorMapping : Profile
    {
        public VendorMapping()
        {
            CreateMap<VendedorDto, Vendedor>().ReverseMap();
        }
    }
}
