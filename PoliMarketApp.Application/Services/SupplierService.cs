using AutoMapper;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly IProveedorRepository _supplierRepository;
    private readonly ICompraProveedorRepository _purchaseRepository;
    private readonly IProductoRepository _productRepository;
    private readonly IMovimientoBodegaRepository _warehouseMovementRepository;
    private readonly IMapper _mapper;

    public SupplierService(
        IProveedorRepository supplierRepository,
        ICompraProveedorRepository purchaseRepository,
        IProductoRepository productRepository,
        IMovimientoBodegaRepository warehouseMovementRepository,
        IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _warehouseMovementRepository = warehouseMovementRepository;
        _mapper = mapper;
    }

    public async Task<ProveedorDto?> CreateSupplierAsync(CreateProveedorDto supplierDto, CancellationToken cancellationToken = default)
    {
        // Verificar si el NIT ya existe
        var existingSupplier = await _supplierRepository.GetByNitAsync(supplierDto.Nit, cancellationToken);
        if (existingSupplier != null)
            return null;

        var supplier = _mapper.Map<Proveedor>(supplierDto);
        supplier.Activo = true;

        await _supplierRepository.AddAsync(supplier, cancellationToken);
        await _supplierRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProveedorDto>(supplier);
    }

    public async Task<IEnumerable<ProveedorDto>> GetActiveSuppliersAsync(CancellationToken cancellationToken = default)
    {
        var suppliers = await _supplierRepository.GetProveedoresActivosAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProveedorDto>>(suppliers);
    }

    public async Task<ProveedorDto?> GetSupplierByIdAsync(int supplierId, CancellationToken cancellationToken = default)
    {
        var supplier = await _supplierRepository.GetByIdAsync(supplierId, cancellationToken);
        return supplier == null ? null : _mapper.Map<ProveedorDto>(supplier);
    }

    public async Task<IEnumerable<ProveedorDto>> GetSuppliersByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        // Obtener proveedores que han suministrado este producto
        var purchases = await _purchaseRepository.GetAllAsync(cancellationToken);
        var supplierIds = purchases
            .Where(p => p.DetalleComprasProveedores.Any(d => d.ProductoId == productId))
            .Select(p => p.ProveedorId)
            .Distinct();

        var suppliers = new List<Proveedor>();
        foreach (var supplierId in supplierIds)
        {
            var supplier = await _supplierRepository.GetByIdAsync(supplierId, cancellationToken);
            if (supplier != null && supplier.Activo)
                suppliers.Add(supplier);
        }

        return _mapper.Map<IEnumerable<ProveedorDto>>(suppliers);
    }

    public async Task<CompraProveedorDto?> CreatePurchaseOrderAsync(CreateCompraProveedorDto purchaseDto, CancellationToken cancellationToken = default)
    {
        var supplier = await _supplierRepository.GetByIdAsync(purchaseDto.ProveedorId, cancellationToken);
        if (supplier == null || !supplier.Activo)
            return null;

        var purchase = new CompraProveedor
        {
            ProveedorId = purchaseDto.ProveedorId,
            FechaCompra = DateTime.Now,
            EstadoCompraProveedorId = 1, // Estado: Pendiente o Registrado
            DetalleComprasProveedores = new List<DetalleCompraProveedor>()
        };

        decimal total = 0;
        foreach (var detail in purchaseDto.Detalles)
        {
            var product = await _productRepository.GetByIdAsync(detail.ProductoId, cancellationToken);
            if (product == null) continue;

            var purchaseDetail = new DetalleCompraProveedor
            {
                ProductoId = detail.ProductoId,
                Cantidad = detail.Cantidad,
                CostoUnitario = detail.CostoUnitario,
                Subtotal = detail.Cantidad * detail.CostoUnitario
            };

            purchase.DetalleComprasProveedores.Add(purchaseDetail);
            total += purchaseDetail.Subtotal;
        }

        purchase.Total = total;

        await _purchaseRepository.AddAsync(purchase, cancellationToken);
        await _purchaseRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompraProveedorDto>(purchase);
    }

    public async Task<IEnumerable<CompraProveedorDto>> GetPurchasesBySupplierAsync(int supplierId, CancellationToken cancellationToken = default)
    {
        var purchases = await _purchaseRepository.GetByProveedorIdAsync(supplierId, cancellationToken);
        return _mapper.Map<IEnumerable<CompraProveedorDto>>(purchases);
    }

    public async Task<CompraProveedorDto?> GetPurchaseDetailsAsync(int purchaseId, CancellationToken cancellationToken = default)
    {
        var purchase = await _purchaseRepository.GetByIdWithDetailsAsync(purchaseId, cancellationToken);
        return purchase == null ? null : _mapper.Map<CompraProveedorDto>(purchase);
    }

    public async Task<bool> RegisterProductsInWarehouseAsync(int purchaseId, CancellationToken cancellationToken = default)
    {
        var purchase = await _purchaseRepository.GetByIdWithDetailsAsync(purchaseId, cancellationToken);
        if (purchase == null) return false;

        foreach (var detail in purchase.DetalleComprasProveedores)
        {
            // Actualizar stock del producto
            var product = await _productRepository.GetByIdAsync(detail.ProductoId, cancellationToken);
            if (product != null)
            {
                product.StockActual += detail.Cantidad;
                _productRepository.Update(product);
            }

            // Registrar movimiento de entrada en bodega
            var warehouseMovement = new MovimientoBodega
            {
                ProductoId = detail.ProductoId,
                TipoMovimiento = "ENTRADA",
                Cantidad = detail.Cantidad,
                Fecha = DateTime.Now,
                Origen = "COMPRA_PROVEEDOR",
                Referencia = purchaseId
            };

            await _warehouseMovementRepository.AddAsync(warehouseMovement, cancellationToken);
        }

        await _productRepository.SaveChangesAsync(cancellationToken);
        await _warehouseMovementRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}