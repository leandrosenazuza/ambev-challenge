using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale> GetBySaleNumberAsync(Guid saleNumber, CancellationToken cancellationToken = default);
        Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid saleNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
