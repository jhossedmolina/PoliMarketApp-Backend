namespace PoliMarketApp.Domain.Entities;

public partial class DetalleCompraProveedor
{
    public int DetalleCompraProveedorId { get; set; }

    public int CompraProveedorId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal CostoUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual CompraProveedor CompraProveedor { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
