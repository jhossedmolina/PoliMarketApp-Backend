namespace PoliMarketApp.Application.DTOs;

public class OrdenEntregaDto
{
    public int OrdenEntregaId { get; set; }
    public int PedidoVentaId { get; set; }
    public string? ClienteNombre { get; set; }
    public string? DireccionEntrega { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public int EstadoOrdenEntregaId { get; set; }
    public string? EstadoDescripcion { get; set; }
    public string? Observaciones { get; set; }
}
