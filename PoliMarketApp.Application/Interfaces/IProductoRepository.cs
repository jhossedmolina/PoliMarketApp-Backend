using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface IProductoRepository : IGenericRepository<Producto>
{
    Task<Producto?> GetByCodigoBarrasAsync(string codigoBarras, CancellationToken cancellationToken = default);
    Task<IEnumerable<Producto>> GetProductosActivosAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Producto>> GetProductosBajoStockAsync(CancellationToken cancellationToken = default);
}