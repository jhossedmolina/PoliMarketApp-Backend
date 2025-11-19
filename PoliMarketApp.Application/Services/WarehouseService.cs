using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IProductoRepository _productRepository;
    private readonly IMovimientoBodegaRepository _warehouseMovementRepository;
    private readonly IPedidoVentaRepository _salesOrderRepository;
    private readonly IMapper _mapper;

    public WarehouseService(
        IProductoRepository productRepository,
        IMovimientoBodegaRepository warehouseMovementRepository,
        IPedidoVentaRepository salesOrderRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _warehouseMovementRepository = warehouseMovementRepository;
        _salesOrderRepository = salesOrderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductoDto>> GetProductsAvailabilityAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetProductosActivosAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductoDto>>(products);
    }

    public async Task<ProductoDto?> GetProductAvailabilityAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        return product == null ? null : _mapper.Map<ProductoDto>(product);
    }

    public async Task<bool> RegisterProductOutputAsync(int salesOrderId, CancellationToken cancellationToken = default)
    {
        var salesOrder = await _salesOrderRepository.GetPedidoWithDetailsAsync(salesOrderId, cancellationToken);
        if (salesOrder == null) return false;

        foreach (var detail in salesOrder.DetallePedidosVenta)
        {
            var warehouseMovement = new MovimientoBodega
            {
                ProductoId = detail.ProductoId,
                TipoMovimiento = "SALIDA",
                Cantidad = detail.Cantidad,
                Fecha = DateTime.Now,
                Origen = "PEDIDO_VENTA",
                Referencia = salesOrderId
            };

            await _warehouseMovementRepository.AddAsync(warehouseMovement, cancellationToken);
        }

        await _warehouseMovementRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<ProductoDto>> GetLowStockProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetProductosBajoStockAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductoDto>>(products);
    }
}