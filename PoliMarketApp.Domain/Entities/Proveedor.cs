namespace PoliMarketApp.Domain.Entities;

public partial class Proveedor
{
    public int ProveedorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Nit { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<CompraProveedor> ComprasProveedores { get; set; } = new List<CompraProveedor>();
}