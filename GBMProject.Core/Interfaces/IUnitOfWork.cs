namespace GBMProject.Core.Interfaces;

public interface IUnitOfWork
{
    ITruckRepository Trucks { get; }
    IDriverRepository Drivers { get; }
    IDeliveryRepository Deliveries { get; }

    Task CommitAsync(CancellationToken cancellationToken);
}