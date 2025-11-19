namespace PoliMarketApp.Domain.Entities;

public partial class Vendedor
{
    public int VendedorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Activo { get; set; }

    public bool AutorizadoParaOperar { get; set; }

    public DateTime FechaIngreso { get; set; }

    public virtual ICollection<PedidoVenta> PedidosVenta { get; set; } = new List<PedidoVenta>();
}