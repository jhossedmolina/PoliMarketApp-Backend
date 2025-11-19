namespace PoliMarketApp.Domain.Entities;

public partial class EstadoCompraProveedor
{
    public int EstadoCompraProveedorId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool EsActivo { get; set; }

    public virtual ICollection<CompraProveedor> CompraProveedores { get; set; } = new List<CompraProveedor>();
}
