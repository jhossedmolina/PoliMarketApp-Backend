using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface IProveedorRepository : IGenericRepository<Proveedor>
{
    Task<Proveedor?> GetByNitAsync(string nit, CancellationToken cancellationToken = default);
    Task<IEnumerable<Proveedor>> GetProveedoresActivosAsync(CancellationToken cancellationToken = default);
}