using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.Data;

namespace PoliMarketApp.Infrastructure.Repositories;

public class CompraProveedorRepository : GenericRepository<CompraProveedor>, ICompraProveedorRepository
{
    public CompraProveedorRepository(PoliMarketDbContext context) : base(context)
    {
    }

    public async Task<CompraProveedor?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Proveedor)
            .Include(c => c.EstadoCompraProveedor)
            .Include(c => c.DetalleComprasProveedores)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(c => c.CompraProveedorId == id, cancellationToken);
    }

    public async Task<IEnumerable<CompraProveedor>> GetByProveedorIdAsync(int proveedorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.EstadoCompraProveedor)
            .Where(c => c.ProveedorId == proveedorId)
            .OrderByDescending(c => c.FechaCompra)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CompraProveedor>> GetByEstadoAsync(int estadoId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Proveedor)
            .Include(c => c.EstadoCompraProveedor)
            .Where(c => c.EstadoCompraProveedorId == estadoId)
            .OrderByDescending(c => c.FechaCompra)
            .ToListAsync(cancellationToken);
    }
}
