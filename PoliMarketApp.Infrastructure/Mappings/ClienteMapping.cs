using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Mappings
{
    public class ClienteMapping : Profile
    {
        public ClienteMapping()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}
