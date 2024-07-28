namespace GBMProject.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ITruckRepository Trucks { get; }
    IDriverRepository Drivers { get; }
    IDeliveryRepository Deliveries { get; }

    Task CommitAsync(CancellationToken cancellationToken);
}