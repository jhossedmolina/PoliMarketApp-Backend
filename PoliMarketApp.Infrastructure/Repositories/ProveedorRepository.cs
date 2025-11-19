using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.Data;

namespace PoliMarketApp.Infrastructure.Repositories
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(PoliMarketDbContext context) : base(context)
        {
        }

        public async Task<Proveedor?> GetByNitAsync(string nit, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.Nit == nit, cancellationToken);
        }

        public async Task<IEnumerable<Proveedor>> GetProveedoresActivosAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(p => p.Activo)
                .ToListAsync(cancellationToken);
        }
    }
}
