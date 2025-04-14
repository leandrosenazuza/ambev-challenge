using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new product in the repository
        /// </summary>
        /// <param name="product">The product to create</param>
        /// <returns>The created product</returns>
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <returns>The product if found, null otherwise</returns>
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

 
        Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves products by category
        /// </summary>
        /// <returns>A list of products in the specified category</returns>
        Task<IEnumerable<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing product
        /// </summary>
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a product from the repository
        /// </summary>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all products with optional filtering and pagination
        /// </summary>
        /// <returns>A list of products</returns>
        Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts the total number of products
        /// </summary>
        /// <returns>The total number of products</returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}