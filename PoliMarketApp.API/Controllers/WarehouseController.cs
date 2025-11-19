using Microsoft.AspNetCore.Mvc;
using PoliMarketApp.Application.Interfaces;

namespace PoliMarketApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet("products/availability")]
    public async Task<IActionResult> GetProductsAvailability(CancellationToken cancellationToken)
    {
        var productos = await _warehouseService.GetProductsAvailabilityAsync(cancellationToken);
        return Ok(productos);
    }

    [HttpGet("products/{productId}/availability")]
    public async Task<IActionResult> GetProductAvailability(int productId, CancellationToken cancellationToken)
    {
        var producto = await _warehouseService.GetProductAvailabilityAsync(productId, cancellationToken);
        if (producto == null)
            return NotFound();

        return Ok(producto);
    }

    [HttpGet("products/low-stock")]
    public async Task<IActionResult> GetLowStockProducts(CancellationToken cancellationToken)
    {
        var productos = await _warehouseService.GetLowStockProductsAsync(cancellationToken);
        return Ok(productos);
    }

    [HttpPost("register-output/{salesOrderId}")]
    public async Task<IActionResult> RegisterProductOutput(int salesOrderId, CancellationToken cancellationToken)
    {
        var result = await _warehouseService.RegisterProductOutputAsync(salesOrderId, cancellationToken);
        if (!result)
            return NotFound(new { message = "Sales order not found" });

        return Ok(new { message = "Product output registered successfully" });
    }
}