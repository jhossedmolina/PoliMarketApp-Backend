using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IOrdenEntregaRepository _deliveryOrderRepository;
    private readonly IPedidoVentaRepository _salesOrderRepository;
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;

    public DeliveryService(
        IOrdenEntregaRepository deliveryOrderRepository,
        IPedidoVentaRepository salesOrderRepository,
        IWarehouseService warehouseService,
        IMapper mapper)
    {
        _deliveryOrderRepository = deliveryOrderRepository;
        _salesOrderRepository = salesOrderRepository;
        _warehouseService = warehouseService;
        _mapper = mapper;
    }

    public async Task<OrdenEntregaDto?> CreateDeliveryOrderAsync(int salesOrderId, string deliveryAddress, CancellationToken cancellationToken = default)
    {
        var salesOrder = await _salesOrderRepository.GetByIdAsync(salesOrderId, cancellationToken);
        if (salesOrder == null) return null;

        var deliveryOrder = new OrdenEntrega
        {
            PedidoVentaId = salesOrderId,
            DireccionEntrega = deliveryAddress,
            FechaProgramada = DateTime.Now,
            EstadoOrdenEntregaId = 1 // Initial state
        };

        await _deliveryOrderRepository.AddAsync(deliveryOrder, cancellationToken);
        await _deliveryOrderRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrdenEntregaDto>(deliveryOrder);
    }

    public async Task<IEnumerable<OrdenEntregaDto>> GetPendingDeliveriesAsync(CancellationToken cancellationToken = default)
    {
        var deliveryOrders = await _deliveryOrderRepository.GetOrdenesPendientesAsync(cancellationToken);
        return _mapper.Map<IEnumerable<OrdenEntregaDto>>(deliveryOrders);
    }

    public async Task<bool> CompleteDeliveryAsync(int deliveryOrderId, CancellationToken cancellationToken = default)
    {
        var deliveryOrder = await _deliveryOrderRepository.GetByIdAsync(deliveryOrderId, cancellationToken);
        if (deliveryOrder == null) return false;

        // Register product output in warehouse
        await _warehouseService.RegisterProductOutputAsync(deliveryOrder.PedidoVentaId, cancellationToken);

        deliveryOrder.FechaEntregaReal = DateTime.Now;
        deliveryOrder.EstadoOrdenEntregaId = 3; // Status: Delivered
        
        _deliveryOrderRepository.Update(deliveryOrder);
        await _deliveryOrderRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<OrdenEntregaDto?> GetDeliveryOrderDetailsAsync(int deliveryOrderId, CancellationToken cancellationToken = default)
    {
        var deliveryOrder = await _deliveryOrderRepository.GetByIdAsync(deliveryOrderId, cancellationToken);
        return deliveryOrder == null ? null : _mapper.Map<OrdenEntregaDto>(deliveryOrder);
    }
}