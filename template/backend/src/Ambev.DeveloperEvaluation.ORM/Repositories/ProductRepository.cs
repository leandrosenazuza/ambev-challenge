using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            return await AddAsync(product, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (product == null) return false;

            _context.Products.Remove(product);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0;
        }

        public async Task<PaginatedResult<Product>> GetAllAsync(PaginationParameters parameters, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            query = ApplyFilters(query, parameters);
            query = ApplyOrdering(query, parameters.OrderBy);

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Product>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = parameters.Page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)parameters.PageSize)
            };
        }

        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, PaginationParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(p => p.Title.Contains(parameters.Name));
            }

            if (!string.IsNullOrEmpty(parameters.Category))
            {
                query = query.Where(p => p.Category == parameters.Category);
            }

            if (parameters.MinTotal.HasValue)
            {
                query = query.Where(p => p.Price >= parameters.MinTotal.Value);
            }

            if (parameters.MaxTotal.HasValue)
            {
                query = query.Where(p => p.Price <= parameters.MaxTotal.Value);
            }

            return query;
        }

        private IQueryable<Product> ApplyOrdering(IQueryable<Product> query, string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return query.OrderBy(p => p.Id);
            }

            var orderClauses = orderBy.Split(',');
            var ordered = false;

            foreach (var clause in orderClauses)
            {
                var trimmedClause = clause.Trim();
                var descending = trimmedClause.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
                var propertyName = descending ? trimmedClause[..^5].Trim() : trimmedClause;

                query = (propertyName.ToLower(), ordered) switch
                {
                    ("price", false) => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    ("price", true) => descending ? ((IOrderedQueryable<Product>)query).ThenByDescending(p => p.Price) : ((IOrderedQueryable<Product>)query).ThenBy(p => p.Price),
                    ("title", false) => descending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                    ("title", true) => descending ? ((IOrderedQueryable<Product>)query).ThenByDescending(p => p.Title) : ((IOrderedQueryable<Product>)query).ThenBy(p => p.Title),
                    ("category", false) => descending ? query.OrderByDescending(p => p.Category) : query.OrderBy(p => p.Category),
                    ("category", true) => descending ? ((IOrderedQueryable<Product>)query).ThenByDescending(p => p.Category) : ((IOrderedQueryable<Product>)query).ThenBy(p => p.Category),
                    _ => query
                };

                ordered = true;
            }

            return query;
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                throw new BadHttpRequestException($"Product with ID {id} not found.");

            return product;
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

            if (existingProduct == null)
                throw new BadHttpRequestException($"Product with ID {product.Id} not found.");

            _context.Entry(existingProduct).State = EntityState.Detached;
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
    }
}