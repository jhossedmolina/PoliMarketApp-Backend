using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.DataAccess;

namespace PoliMarketApp.Infrastructure.Repositories;

public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
{
    public ProductoRepository(PoliMarketDbContext context) : base(context)
    {
    }

    public async Task<Producto?> GetByCodigoBarrasAsync(string codigoBarras, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.CodigoBarras == codigoBarras, cancellationToken);
    }

    public async Task<IEnumerable<Producto>> GetProductosActivosAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Activo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Producto>> GetProductosBajoStockAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.StockActual <= p.StockMinimo && p.Activo)
            .ToListAsync(cancellationToken);
    }
}
