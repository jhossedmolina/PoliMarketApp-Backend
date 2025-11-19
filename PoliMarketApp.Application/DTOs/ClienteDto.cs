namespace PoliMarketApp.Application.DTOs;

public class ClienteDto
{
    public int ClienteId { get; set; }
    public string Nombre { get; set; } = null!;
    public string Documento { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; }
}
