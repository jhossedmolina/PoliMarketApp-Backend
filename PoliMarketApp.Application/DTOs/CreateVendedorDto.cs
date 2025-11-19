namespace PoliMarketApp.Application.DTOs;

public class CreateVendedorDto
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Documento { get; set; } = null!;
    public string Email { get; set; } = null!;
}