namespace PoliMarketApp.Application.DTOs;

public class CreatePedidoVentaDto
{
    public int VendedorId { get; set; }
    public int ClienteId { get; set; }
    public string? Observaciones { get; set; }
    public List<DetallePedidoVentaDto> Detalles { get; set; } = new();
}

public class DetallePedidoVentaDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
