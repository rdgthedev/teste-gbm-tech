using GBMProject.Core.Entities;
using GBMProject.Core.Enums;

namespace GBMProject.Core.Interfaces;

public interface IDeliveryRepository
{
    Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> GetAllWithDriversAndTrucksAsync(CancellationToken cancellationToken);
    Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Delivery?> GetByIdWithDriversAndTrucksAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> GetCreatedOrInProgressDeliveriesByTruckIdAndDate(Guid truckId, DateTime date, CancellationToken cancellationToken);
    Task<bool> GetCreatedOrInProgressDeliveriesByDriverIdAndDate(Guid driverId, DateTime deliveryDate, CancellationToken cancellationToken);
    Task CreateAsync(Delivery delivery, CancellationToken cancellationToken);
    void Update(Delivery driver);
}