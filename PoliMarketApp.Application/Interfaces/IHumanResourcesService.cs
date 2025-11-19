using PoliMarketApp.Application.DTOs;

namespace PoliMarketApp.Application.Interfaces;

public interface IHumanResourcesService
{
    Task<bool> AuthorizeVendorAsync(int vendedorId, CancellationToken cancellationToken = default);
    Task<bool> RevokeVendorAuthorizationAsync(int vendedorId, CancellationToken cancellationToken = default);
    Task<VendedorDto?> GetVendorByIdAsync(int vendedorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<VendedorDto>> GetAuthorizedVendorsAsync(CancellationToken cancellationToken = default);
}