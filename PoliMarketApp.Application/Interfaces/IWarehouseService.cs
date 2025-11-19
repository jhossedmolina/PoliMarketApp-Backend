using PoliMarketApp.Application.DTOs;

namespace PoliMarketApp.Application.Interfaces;

public interface IWarehouseService
{
    Task<IEnumerable<ProductoDto>> GetProductsAvailabilityAsync(CancellationToken cancellationToken = default);
    Task<ProductoDto?> GetProductAvailabilityAsync(int productoId, CancellationToken cancellationToken = default);
    Task<bool> RegisterProductOutputAsync(int pedidoVentaId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductoDto>> GetLowStockProductsAsync(CancellationToken cancellationToken = default);
}