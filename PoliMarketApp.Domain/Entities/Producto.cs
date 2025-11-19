namespace PoliMarketApp.Domain.Entities;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string? CodigoBarras { get; set; }

    public int StockActual { get; set; }

    public int StockMinimo { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<DetalleCompraProveedor> DetalleComprasProveedors { get; set; } = new List<DetalleCompraProveedor>();

    public virtual ICollection<DetallePedidoVenta> DetallePedidosVenta { get; set; } = new List<DetallePedidoVenta>();

    public virtual ICollection<MovimientoBodega> MovimientosBodegas { get; set; } = new List<MovimientoBodega>();
}