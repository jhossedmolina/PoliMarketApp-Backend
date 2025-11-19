using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface IMovimientoBodegaRepository : IGenericRepository<MovimientoBodega>
{
    Task<IEnumerable<MovimientoBodega>> GetByProductoIdAsync(int productoId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MovimientoBodega>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default);
    Task<IEnumerable<MovimientoBodega>> GetByTipoMovimientoAsync(string tipoMovimiento, CancellationToken cancellationToken = default);
}