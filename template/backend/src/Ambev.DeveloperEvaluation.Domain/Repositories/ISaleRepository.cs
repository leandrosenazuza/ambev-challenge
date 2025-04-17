using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale> GetBySaleNumberAsync(Guid saleNumber, CancellationToken cancellationToken = default);
        Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid saleNumber, CancellationToken cancellationToken = default);
        Task<PaginatedResult<Sale>> GetAllAsync(PaginationParameters parameters, CancellationToken cancellationToken = default);
    }
}
