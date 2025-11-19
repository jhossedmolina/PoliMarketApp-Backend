using PoliMarketApp.Application.DTOs;

namespace PoliMarketApp.Application.Interfaces;

public interface ISalesService
{
    Task<PedidoVentaDto?> CreateSalesOrderAsync(CreatePedidoVentaDto pedidoDto, CancellationToken cancellationToken = default);
    Task<IEnumerable<PedidoVentaDto>> GetSalesOrdersByVendorAsync(int vendedorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ClienteDto>> GetAvailableCustomersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductoDto>> GetAvailableProductsAsync(CancellationToken cancellationToken = default);
    Task<PedidoVentaDto?> GetSalesOrderDetailsAsync(int pedidoVentaId, CancellationToken cancellationToken = default);
}