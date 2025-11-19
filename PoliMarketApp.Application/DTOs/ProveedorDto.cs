namespace PoliMarketApp.Application.DTOs;

public class ProveedorDto
{
    public int ProveedorId { get; set; }
    public string Nombre { get; set; } = null!;
    public string Nit { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; }
}