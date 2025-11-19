using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces
{
    public interface IPedidoVentaRepository : IGenericRepository<PedidoVenta>
    {
        Task<IEnumerable<PedidoVenta>> GetPedidosByVendedorAsync(int vendedorId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PedidoVenta>> GetPedidosByClienteAsync(int clienteId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PedidoVenta>> GetPedidosByEstadoAsync(int estadoId, CancellationToken cancellationToken = default);
        Task<PedidoVenta?> GetPedidoWithDetailsAsync(int pedidoVentaId, CancellationToken cancellationToken = default);
    }
}
