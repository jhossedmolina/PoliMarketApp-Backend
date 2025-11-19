namespace PoliMarketApp.Application.DTOs;

public class CreateCompraProveedorDto
{
    public int ProveedorId { get; set; }
    public List<DetalleCompraProveedorDto> Detalles { get; set; } = new();
}