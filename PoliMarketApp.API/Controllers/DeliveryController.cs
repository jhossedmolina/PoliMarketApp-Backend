using Microsoft.AspNetCore.Mvc;
using PoliMarketApp.Application.Interfaces;

namespace PoliMarketApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;

    public DeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpPost("orders")]
    public async Task<IActionResult> CreateDeliveryOrder([FromBody] CreateDeliveryOrderRequest request, CancellationToken cancellationToken)
    {
        var orden = await _deliveryService.CreateDeliveryOrderAsync(
            request.PedidoVentaId, 
            request.DireccionEntrega, 
            cancellationToken);

        if (orden == null)
            return NotFound(new { message = "Sales order not found" });

        return CreatedAtAction(nameof(GetDeliveryOrderDetails), new { orderId = orden.OrdenEntregaId }, orden);
    }

    [HttpGet("orders/pending")]
    public async Task<IActionResult> GetPendingDeliveries(CancellationToken cancellationToken)
    {
        var ordenes = await _deliveryService.GetPendingDeliveriesAsync(cancellationToken);
        return Ok(ordenes);
    }

    [HttpGet("orders/{orderId}")]
    public async Task<IActionResult> GetDeliveryOrderDetails(int orderId, CancellationToken cancellationToken)
    {
        var orden = await _deliveryService.GetDeliveryOrderDetailsAsync(orderId, cancellationToken);
        if (orden == null)
            return NotFound();

        return Ok(orden);
    }

    [HttpPost("orders/{orderId}/complete")]
    public async Task<IActionResult> CompleteDelivery(int orderId, CancellationToken cancellationToken)
    {
        var result = await _deliveryService.CompleteDeliveryAsync(orderId, cancellationToken);
        if (!result)
            return NotFound(new { message = "Delivery order not found" });

        return Ok(new { message = "Delivery completed successfully" });
    }
}

public record CreateDeliveryOrderRequest(int PedidoVentaId, string DireccionEntrega);