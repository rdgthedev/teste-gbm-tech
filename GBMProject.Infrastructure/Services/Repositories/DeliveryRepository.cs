using GBMProject.Core.Entities;
using GBMProject.Core.Enums;
using GBMProject.Core.Interfaces;
using GBMProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GBMProject.Infrastructure.Services.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly GbmProjectDbContext _context;

    public DeliveryRepository(GbmProjectDbContext context)
        => _context = context;

    public async Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken cancellationToken)
        => await _context
            .Deliveries
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context
            .Deliveries
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<bool> GetCreatedOrInProgressDeliveriesByTruckIdAndDate(Guid truckId, DateTime deliveryDate,
        CancellationToken cancellationToken)
        => await _context
            .Deliveries
            .AsNoTracking()
            .AnyAsync(d => d.TruckId == truckId
                           && d.DeliveryDate.Date == deliveryDate.Date
                           && (d.DeliveryStatus == EDeliveryStatus.Created ||
                               d.DeliveryStatus == EDeliveryStatus.InProgress), cancellationToken);

    public async Task CreateAsync(Delivery delivery, CancellationToken cancellationToken)
        => await _context
            .Deliveries
            .AddAsync(delivery, cancellationToken);

    public void Update(Delivery driver)
        => _context.Deliveries.Update(driver);
}