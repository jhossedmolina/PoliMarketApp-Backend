using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.Data;

namespace PoliMarketApp.Infrastructure.Repositories;

public class PedidoVentaRepository : GenericRepository<PedidoVenta>, IPedidoVentaRepository
{
    public PedidoVentaRepository(PoliMarketDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PedidoVenta>> GetPedidosByVendedorAsync(int vendedorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.VendedorId == vendedorId)
            .Include(p => p.Cliente)
            .Include(p => p.EstadoPedidoVenta)
            .OrderByDescending(p => p.FechaCreacion)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PedidoVenta>> GetPedidosByClienteAsync(int clienteId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.ClienteId == clienteId)
            .Include(p => p.Vendedor)
            .Include(p => p.EstadoPedidoVenta)
            .OrderByDescending(p => p.FechaCreacion)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PedidoVenta>> GetPedidosByEstadoAsync(int estadoId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.EstadoPedidoVentaId == estadoId)
            .Include(p => p.Cliente)
            .Include(p => p.Vendedor)
            .ToListAsync(cancellationToken);
    }

    public async Task<PedidoVenta?> GetPedidoWithDetailsAsync(int pedidoVentaId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Vendedor)
            .Include(p => p.EstadoPedidoVenta)
            .Include(p => p.DetallePedidosVenta)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(p => p.PedidoVentaId == pedidoVentaId, cancellationToken);
    }
}
