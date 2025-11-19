namespace PoliMarketApp.Application.DTOs;

public class CompraProveedorDto
{
    public int CompraProveedorId { get; set; }
    public int ProveedorId { get; set; }
    public string? ProveedorNombre { get; set; }
    public DateTime FechaCompra { get; set; }
    public decimal Total { get; set; }
    public int EstadoCompraProveedorId { get; set; }
    public string? EstadoDescripcion { get; set; }
    public List<DetalleCompraProveedorDto>? Detalles { get; set; }
}