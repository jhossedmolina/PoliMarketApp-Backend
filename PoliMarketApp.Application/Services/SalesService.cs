using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Services;

public class SalesService : ISalesService
{
    private readonly IPedidoVentaRepository _salesOrderRepository;
    private readonly IClienteRepository _customerRepository;
    private readonly IProductoRepository _productRepository;
    private readonly IVendedorRepository _vendorRepository;
    private readonly IMapper _mapper;

    public SalesService(
        IPedidoVentaRepository salesOrderRepository,
        IClienteRepository customerRepository,
        IProductoRepository productRepository,
        IVendedorRepository vendorRepository,
        IMapper mapper)
    {
        _salesOrderRepository = salesOrderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _vendorRepository = vendorRepository;
        _mapper = mapper;
    }

    public async Task<PedidoVentaDto?> CreateSalesOrderAsync(CreatePedidoVentaDto orderDto, CancellationToken cancellationToken = default)
    {
        // Validate vendor is authorized
        var vendor = await _vendorRepository.GetByIdAsync(orderDto.VendedorId, cancellationToken);
        if (vendor == null || !vendor.AutorizadoParaOperar)
            return null;

        var salesOrder = _mapper.Map<PedidoVenta>(orderDto);
        
        // Calculate total
        salesOrder.Total = salesOrder.DetallePedidosVenta.Sum(d => d.Subtotal);

        await _salesOrderRepository.AddAsync(salesOrder, cancellationToken);
        await _salesOrderRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PedidoVentaDto>(salesOrder);
    }

    public async Task<IEnumerable<PedidoVentaDto>> GetSalesOrdersByVendorAsync(int vendorId, CancellationToken cancellationToken = default)
    {
        var salesOrders = await _salesOrderRepository.GetPedidosByVendedorAsync(vendorId, cancellationToken);
        return _mapper.Map<IEnumerable<PedidoVentaDto>>(salesOrders);
    }

    public async Task<IEnumerable<ClienteDto>> GetAvailableCustomersAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _customerRepository.GetClientesActivosAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ClienteDto>>(customers);
    }

    public async Task<IEnumerable<ProductoDto>> GetAvailableProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetProductosActivosAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductoDto>>(products);
    }

    public async Task<PedidoVentaDto?> GetSalesOrderDetailsAsync(int salesOrderId, CancellationToken cancellationToken = default)
    {
        var salesOrder = await _salesOrderRepository.GetPedidoWithDetailsAsync(salesOrderId, cancellationToken);
        return salesOrder == null ? null : _mapper.Map<PedidoVentaDto>(salesOrder);
    }
}