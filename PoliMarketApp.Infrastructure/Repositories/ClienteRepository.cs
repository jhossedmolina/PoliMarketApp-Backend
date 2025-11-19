using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Domain.Entities;
using PoliMarketApp.Infrastructure.Data;

namespace PoliMarketApp.Infrastructure.Repositories;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(PoliMarketDbContext context) : base(context)
    {
    }

    public async Task<Cliente?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Documento == documento, cancellationToken);
    }

    public async Task<IEnumerable<Cliente>> GetClientesActivosAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.Activo)
            .ToListAsync(cancellationToken);
    }
}
