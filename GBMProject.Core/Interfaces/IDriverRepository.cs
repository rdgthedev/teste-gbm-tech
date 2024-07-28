using GBMProject.Core.Entities;

namespace GBMProject.Core.Interfaces;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync(CancellationToken cancellationToken);
    Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<bool> GetByPhoneAsync(string phone, CancellationToken cancellationToken);
    Task CreateAsync(Driver driver, CancellationToken cancellationToken);
    void Update(Driver driver);
}