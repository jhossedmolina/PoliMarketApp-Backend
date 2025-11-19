namespace PoliMarketApp.Domain.Entities;

public partial class CompraProveedor
{
    public int CompraProveedorId { get; set; }

    public int ProveedorId { get; set; }

    public int EstadoCompraProveedorId { get; set; }

    public DateTime FechaCompra { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetalleCompraProveedor> DetalleComprasProveedores { get; set; } = new List<DetalleCompraProveedor>();

    public virtual EstadoCompraProveedor EstadoCompraProveedor { get; set; } = null!;

    public virtual Proveedor Proveedor { get; set; } = null!;
}