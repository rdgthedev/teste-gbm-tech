using GBMProject.Core.Interfaces;
using GBMProject.Infrastructure.Persistence;

namespace GBMProject.Infrastructure.Services.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GbmProjectDbContext _context;
    private ITruckRepository _truckRepository = null!;
    private IDriverRepository _driverRepository = null!;
    private IDeliveryRepository _deliveryRepository = null!;

    public UnitOfWork(GbmProjectDbContext context)
        => _context = context;

    public ITruckRepository Trucks => _truckRepository ??= new TruckRepository(_context);
    public IDriverRepository Drivers => _driverRepository ??= new DriverRepository(_context);
    public IDeliveryRepository Deliveries => _deliveryRepository ??= new DeliveryRepository(_context);

    public async Task CommitAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
        => _context.Dispose();
}