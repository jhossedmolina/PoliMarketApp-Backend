namespace PoliMarketApp.Domain.Entities;

public partial class OrdenEntrega
{
    public int OrdenEntregaId { get; set; }

    public int PedidoVentaId { get; set; }

    public int EstadoOrdenEntregaId { get; set; }

    public DateTime FechaProgramada { get; set; }

    public DateTime? FechaEntregaReal { get; set; }

    public string? DireccionEntrega { get; set; }

    public virtual EstadoOrdenEntrega EstadoOrdenEntrega { get; set; } = null!;

    public virtual PedidoVenta PedidoVenta { get; set; } = null!;
}
