using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public interface IProductRepository
    {

        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}