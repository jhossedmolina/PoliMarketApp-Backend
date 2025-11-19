using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Application.Interfaces;

public interface ICompraProveedorRepository : IGenericRepository<CompraProveedor>
{
    Task<CompraProveedor?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CompraProveedor>> GetByProveedorIdAsync(int proveedorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CompraProveedor>> GetByEstadoAsync(int estadoId, CancellationToken cancellationToken = default);
}