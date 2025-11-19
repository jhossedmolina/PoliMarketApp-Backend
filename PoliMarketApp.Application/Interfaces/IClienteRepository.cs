using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cliente>> GetClientesActivosAsync(CancellationToken cancellationToken = default);
}
