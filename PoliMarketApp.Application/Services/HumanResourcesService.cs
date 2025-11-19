using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;

namespace PoliMarketApp.Application.Services;

public class HumanResourcesService : IHumanResourcesService
{
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IMapper _mapper;

    public HumanResourcesService(IVendedorRepository vendedorRepository, IMapper mapper)
    {
        _vendedorRepository = vendedorRepository;
        _mapper = mapper;
    }

    public async Task<bool> AuthorizeVendorAsync(int vendedorId, CancellationToken cancellationToken = default)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(vendedorId, cancellationToken);
        if (vendedor == null) return false;

        vendedor.AutorizadoParaOperar = true;
        _vendedorRepository.Update(vendedor);
        await _vendedorRepository.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<bool> RevokeVendorAuthorizationAsync(int vendedorId, CancellationToken cancellationToken = default)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(vendedorId, cancellationToken);
        if (vendedor == null) return false;

        vendedor.AutorizadoParaOperar = false;
        _vendedorRepository.Update(vendedor);
        await _vendedorRepository.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<VendedorDto?> GetVendorByIdAsync(int vendedorId, CancellationToken cancellationToken = default)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(vendedorId, cancellationToken);
        return vendedor == null ? null : _mapper.Map<VendedorDto>(vendedor);
    }

    public async Task<IEnumerable<VendedorDto>> GetAuthorizedVendorsAsync(CancellationToken cancellationToken = default)
    {
        var vendedores = await _vendedorRepository.GetVendedoresAutorizadosAsync(cancellationToken);
        return _mapper.Map<IEnumerable<VendedorDto>>(vendedores);
    }
}