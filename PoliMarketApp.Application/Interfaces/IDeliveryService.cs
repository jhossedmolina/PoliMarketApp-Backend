using PoliMarketApp.Application.DTOs;

namespace PoliMarketApp.Application.Interfaces;

public interface IDeliveryService
{
    Task<OrdenEntregaDto?> CreateDeliveryOrderAsync(int pedidoVentaId, string direccionEntrega, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrdenEntregaDto>> GetPendingDeliveriesAsync(CancellationToken cancellationToken = default);
    Task<bool> CompleteDeliveryAsync(int ordenEntregaId, CancellationToken cancellationToken = default);
    Task<OrdenEntregaDto?> GetDeliveryOrderDetailsAsync(int ordenEntregaId, CancellationToken cancellationToken = default);
}