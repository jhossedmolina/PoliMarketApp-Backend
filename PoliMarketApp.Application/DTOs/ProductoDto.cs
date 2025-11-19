namespace PoliMarketApp.Application.DTOs;

public class ProductoDto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string CodigoBarras { get; set; } = null!;
    public decimal PrecioVenta { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public bool Activo { get; set; }
}
