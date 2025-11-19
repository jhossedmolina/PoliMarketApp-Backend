namespace PoliMarketApp.Application.DTOs
{
    public class PedidoVentaDto
    {
        public int PedidoVentaId { get; set; }
        public int VendedorId { get; set; }
        public string? VendedorNombre { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNombre { get; set; }
        public DateTime FechaPedido { get; set; }
        public int EstadoPedidoVentaId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public decimal Total { get; set; }
        public string? Observaciones { get; set; }
    }
}
