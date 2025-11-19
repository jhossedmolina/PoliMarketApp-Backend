namespace PoliMarketApp.Domain.Entities;

public partial class PedidoVenta
{
    public int PedidoVentaId { get; set; }

    public int VendedorId { get; set; }

    public int ClienteId { get; set; }

    public int EstadoPedidoVentaId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<DetallePedidoVenta> DetallePedidosVenta { get; set; } = new List<DetallePedidoVenta>();

    public virtual EstadoPedidoVenta EstadoPedidoVenta { get; set; } = null!;

    public virtual ICollection<OrdenEntrega> OrdenesEntregas { get; set; } = new List<OrdenEntrega>();

    public virtual Vendedor Vendedor { get; set; } = null!;
}
