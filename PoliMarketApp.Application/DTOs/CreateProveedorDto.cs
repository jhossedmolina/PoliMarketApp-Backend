namespace PoliMarketApp.Application.DTOs;

public class CreateProveedorDto
{
    public string Nombre { get; set; } = null!;
    public string Nit { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}