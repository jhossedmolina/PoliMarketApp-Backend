namespace PoliMarketApp.Domain.Entities;

public partial class MovimientoBodega
{
    public int MovimientoBodegaId { get; set; }

    public int ProductoId { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public string? Origen { get; set; }

    public int? Referencia { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}