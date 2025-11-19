using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.DataAccess;

namespace PoliMarketApp.Infrastructure.Repositories;

public class OrdenEntregaRepository : GenericRepository<OrdenEntrega>, IOrdenEntregaRepository
{
    public OrdenEntregaRepository(PoliMarketDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrdenEntrega>> GetOrdenesByEstadoAsync(int estadoId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.EstadoOrdenEntregaId == estadoId)
            .Include(o => o.PedidoVenta)
                .ThenInclude(p => p.Cliente)
            .Include(o => o.EstadoOrdenEntrega)
            .OrderByDescending(o => o.FechaEntregaReal)
            .ToListAsync(cancellationToken);
    }

    public async Task<OrdenEntrega?> GetOrdenByPedidoVentaAsync(int pedidoVentaId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(o => o.PedidoVenta)
            .Include(o => o.EstadoOrdenEntrega)
            .FirstOrDefaultAsync(o => o.PedidoVentaId == pedidoVentaId, cancellationToken);
    }

    public async Task<IEnumerable<OrdenEntrega>> GetOrdenesPendientesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.FechaProgramada == default(DateTime))
            .Include(o => o.PedidoVenta)
                .ThenInclude(p => p.Cliente)
            .Include(o => o.EstadoOrdenEntrega)
            .OrderBy(o => o.FechaEntregaReal)
            .ToListAsync(cancellationToken);
    }
}
