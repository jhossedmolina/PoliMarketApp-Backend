using Microsoft.AspNetCore.Mvc;
using PoliMarketApp.Application.DTOs;
using PoliMarketApp.Application.Interfaces;

namespace PoliMarketApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly ISalesService _salesService;

    public SalesController(ISalesService salesService)
    {
        _salesService = salesService;
    }

    [HttpPost("orders")]
    public async Task<IActionResult> CreateSalesOrder([FromBody] CreatePedidoVentaDto pedidoDto, CancellationToken cancellationToken)
    {
        var pedido = await _salesService.CreateSalesOrderAsync(pedidoDto, cancellationToken);
        if (pedido == null)
            return BadRequest(new { message = "Vendor not authorized or not found" });

        return CreatedAtAction(nameof(GetSalesOrderDetails), new { orderId = pedido.PedidoVentaId }, pedido);
    }

    [HttpGet("orders/{orderId}")]
    public async Task<IActionResult> GetSalesOrderDetails(int orderId, CancellationToken cancellationToken)
    {
        var pedido = await _salesService.GetSalesOrderDetailsAsync(orderId, cancellationToken);
        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpGet("vendors/{vendorId}/orders")]
    public async Task<IActionResult> GetSalesOrdersByVendor(int vendorId, CancellationToken cancellationToken)
    {
        var pedidos = await _salesService.GetSalesOrdersByVendorAsync(vendorId, cancellationToken);
        return Ok(pedidos);
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetAvailableCustomers(CancellationToken cancellationToken)
    {
        var clientes = await _salesService.GetAvailableCustomersAsync(cancellationToken);
        return Ok(clientes);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAvailableProducts(CancellationToken cancellationToken)
    {
        var productos = await _salesService.GetAvailableProductsAsync(cancellationToken);
        return Ok(productos);
    }
}