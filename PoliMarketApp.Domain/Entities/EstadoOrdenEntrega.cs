namespace PoliMarketApp.Domain.Entities;

public partial class EstadoOrdenEntrega
{
    public int EstadoOrdenEntregaId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool EsActivo { get; set; }

    public virtual ICollection<OrdenEntrega> OrdenesEntregas { get; set; } = new List<OrdenEntrega>();
}
