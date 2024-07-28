using GBMProject.Core.Entities;
using GBMProject.Core.Enums;

namespace GBMProject.Core.Interfaces;

public interface IDeliveryRepository
{
    Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken cancellationToken);
    Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> GetDeliveriesCreatedOrInProgressByTruckIdAndDate(Guid truckId, DateTime date, CancellationToken cancellationToken);
    Task CreateAsync(Delivery delivery, CancellationToken cancellationToken);
    void Update(Delivery driver);
}