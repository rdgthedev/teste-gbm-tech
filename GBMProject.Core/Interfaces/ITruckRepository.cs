using GBMProject.Core.Entities;

namespace GBMProject.Core.Interfaces;

public interface ITruckRepository
{
    Task<IEnumerable<Truck>> GetAllAsync(CancellationToken cancellationToken);
    Task<Truck?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> PlateExists(string plate, CancellationToken cancellationToken);
    Task CreateAsync(Truck truck, CancellationToken cancellationToken);
    void Update(Truck truck);
}