namespace PoliMarketApp.Domain.Entities; 
public partial class DetallePedidoVenta
{
    public int DetallePedidoVentaId { get; set; }

    public int PedidoVentaId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual PedidoVenta PedidoVenta { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
