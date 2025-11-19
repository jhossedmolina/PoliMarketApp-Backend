using Microsoft.AspNetCore.Mvc;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;

namespace PoliMarketApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    // ========== Gestión de Proveedores ==========

    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateProveedorDto supplierDto, CancellationToken cancellationToken)
    {
        var supplier = await _supplierService.CreateSupplierAsync(supplierDto, cancellationToken);
        if (supplier == null)
            return BadRequest(new { message = "Supplier with this NIT already exists" });

        return CreatedAtAction(nameof(GetSupplierById), new { supplierId = supplier.ProveedorId }, supplier);
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveSuppliers(CancellationToken cancellationToken)
    {
        var suppliers = await _supplierService.GetActiveSuppliersAsync(cancellationToken);
        return Ok(suppliers);
    }

    [HttpGet("{supplierId}")]
    public async Task<IActionResult> GetSupplierById(int supplierId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(supplierId, cancellationToken);
        if (supplier == null)
            return NotFound();

        return Ok(supplier);
    }

    [HttpGet("by-product/{productId}")]
    public async Task<IActionResult> GetSuppliersByProduct(int productId, CancellationToken cancellationToken)
    {
        var suppliers = await _supplierService.GetSuppliersByProductIdAsync(productId, cancellationToken);
        return Ok(suppliers);
    }

    // ========== Gestión de Compras ==========

    [HttpPost("purchases")]
    public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreateCompraProveedorDto purchaseDto, CancellationToken cancellationToken)
    {
        var purchase = await _supplierService.CreatePurchaseOrderAsync(purchaseDto, cancellationToken);
        if (purchase == null)
            return BadRequest(new { message = "Supplier not found or inactive" });

        return CreatedAtAction(nameof(GetPurchaseDetails), new { purchaseId = purchase.CompraProveedorId }, purchase);
    }

    [HttpGet("{supplierId}/purchases")]
    public async Task<IActionResult> GetPurchasesBySupplier(int supplierId, CancellationToken cancellationToken)
    {
        var purchases = await _supplierService.GetPurchasesBySupplierAsync(supplierId, cancellationToken);
        return Ok(purchases);
    }

    [HttpGet("purchases/{purchaseId}")]
    public async Task<IActionResult> GetPurchaseDetails(int purchaseId, CancellationToken cancellationToken)
    {
        var purchase = await _supplierService.GetPurchaseDetailsAsync(purchaseId, cancellationToken);
        if (purchase == null)
            return NotFound();

        return Ok(purchase);
    }

    [HttpPost("purchases/{purchaseId}/register-in-warehouse")]
    public async Task<IActionResult> RegisterProductsInWarehouse(int purchaseId, CancellationToken cancellationToken)
    {
        var result = await _supplierService.RegisterProductsInWarehouseAsync(purchaseId, cancellationToken);
        if (!result)
            return NotFound(new { message = "Purchase not found" });

        return Ok(new { message = "Products registered in warehouse successfully" });
    }
}