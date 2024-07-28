using GBMProject.Core.Entities;
using GBMProject.Core.Interfaces;
using GBMProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GBMProject.Infrastructure.Services.Repositories;

public class TruckRepository : ITruckRepository
{
    private readonly GbmProjectDbContext _context;

    public TruckRepository(GbmProjectDbContext context)
        => _context = context;

    public async Task<IEnumerable<Truck>> GetAllAsync(CancellationToken cancellationToken)
        => await _context
            .Trucks
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Truck?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context
            .Trucks
            .AsNoTracking()
            .FirstOrDefaultAsync(ts => ts.Id == id, cancellationToken);

    public async Task<bool> PlateExists(string plate, CancellationToken cancellationToken)
        => await _context
            .Trucks
            .AsNoTracking()
            .AnyAsync(ts => ts.Plate == plate, cancellationToken);

    public async Task CreateAsync(Truck truck, CancellationToken cancellationToken)
        => await _context.AddAsync(truck, cancellationToken);

    public void Update(Truck truck)
        => _context.Trucks.Update(truck);
}