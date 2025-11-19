namespace PoliMarketApp.Domain.Entities;

public partial class EstadoPedidoVenta
{
    public int EstadoPedidoVentaId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool EsActivo { get; set; }

    public virtual ICollection<PedidoVenta> PedidosVenta { get; set; } = new List<PedidoVenta>();
}

