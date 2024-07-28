using GBMProject.Core.Entities;
using GBMProject.Core.Interfaces;
using GBMProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GBMProject.Infrastructure.Services.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly GbmProjectDbContext _context;

    public DriverRepository(GbmProjectDbContext context)
        => _context = context;

    public async Task<IEnumerable<Driver>> GetAllAsync(CancellationToken cancellationToken)
        => await _context
            .Drivers
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context
            .Drivers
            .AsNoTracking()
            .FirstOrDefaultAsync(ds => ds.Id == id, cancellationToken);

    public async Task<bool> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        => await _context
            .Drivers
            .AsNoTracking()
            .AnyAsync(d => d.Cpf == cpf, cancellationToken);

    public async Task<bool> GetByPhoneAsync(string phone, CancellationToken cancellationToken)
        => await _context
            .Drivers
            .AsNoTracking()
            .AnyAsync(d => d.Phone == phone, cancellationToken);
    public async Task CreateAsync(Driver driver, CancellationToken cancellationToken)
        => await _context.Drivers.AddAsync(driver, cancellationToken);

    public void Update(Driver driver)
        => _context.Drivers.Update(driver);
}