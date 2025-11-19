using PoliMarketApp.Application.DTOs;

namespace PoliMarketApp.Application.Interfaces;

public interface ISupplierService
{
    // Gestión de Proveedores
    Task<ProveedorDto?> CreateSupplierAsync(CreateProveedorDto supplierDto, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProveedorDto>> GetActiveSuppliersAsync(CancellationToken cancellationToken = default);
    Task<ProveedorDto?> GetSupplierByIdAsync(int supplierId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProveedorDto>> GetSuppliersByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    
    // Gestión de Compras a Proveedores
    Task<CompraProveedorDto?> CreatePurchaseOrderAsync(CreateCompraProveedorDto purchaseDto, CancellationToken cancellationToken = default);
    Task<IEnumerable<CompraProveedorDto>> GetPurchasesBySupplierAsync(int supplierId, CancellationToken cancellationToken = default);
    Task<CompraProveedorDto?> GetPurchaseDetailsAsync(int purchaseId, CancellationToken cancellationToken = default);
    Task<bool> RegisterProductsInWarehouseAsync(int purchaseId, CancellationToken cancellationToken = default);
}