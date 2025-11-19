namespace PoliMarketApp.Domain.Entities;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<PedidoVenta> PedidosVenta { get; set; } = new List<PedidoVenta>();
}

