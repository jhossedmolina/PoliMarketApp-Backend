using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.Data;

namespace PoliMarketApp.Infrastructure.Repositories;

public class VendedorRepository : GenericRepository<Vendedor>, IVendedorRepository
{
    public VendedorRepository(PoliMarketDbContext context) : base(context)
    {
    }

    public async Task<Vendedor?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(v => v.Documento == documento, cancellationToken);
    }

    public async Task<IEnumerable<Vendedor>> GetVendedoresActivosAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(v => v.Activo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Vendedor>> GetVendedoresAutorizadosAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(v => v.AutorizadoParaOperar && v.Activo)
            .ToListAsync(cancellationToken);
    }
}
