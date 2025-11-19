using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.DataAccess;

namespace PoliMarketApp.Infrastructure.Repositories
{
    public class MovimientoBodegaRepository : GenericRepository<MovimientoBodega>, IMovimientoBodegaRepository
    {
        public MovimientoBodegaRepository(PoliMarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MovimientoBodega>> GetByProductoIdAsync(int productoId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(m => m.Producto)
                .Where(m => m.ProductoId == productoId)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<MovimientoBodega>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(m => m.Producto)
                .Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<MovimientoBodega>> GetByTipoMovimientoAsync(string tipoMovimiento, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(m => m.Producto)
                .Where(m => m.TipoMovimiento == tipoMovimiento)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync(cancellationToken);
        }
    }
}
