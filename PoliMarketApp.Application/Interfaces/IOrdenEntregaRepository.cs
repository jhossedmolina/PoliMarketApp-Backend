using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface IOrdenEntregaRepository : IGenericRepository<OrdenEntrega>
{
    Task<IEnumerable<OrdenEntrega>> GetOrdenesByEstadoAsync(int estadoId, CancellationToken cancellationToken = default);
    Task<OrdenEntrega?> GetOrdenByPedidoVentaAsync(int pedidoVentaId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrdenEntrega>> GetOrdenesPendientesAsync(CancellationToken cancellationToken = default);
}
