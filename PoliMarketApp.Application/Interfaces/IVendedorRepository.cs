using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface IVendedorRepository : IGenericRepository<Vendedor>
{
    Task<Vendedor?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default);
    Task<IEnumerable<Vendedor>> GetVendedoresActivosAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Vendedor>> GetVendedoresAutorizadosAsync(CancellationToken cancellationToken = default);
}