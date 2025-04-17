using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken)
        {
            return await AddAsync(sale, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid saleNumber, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
            _context.Sales.Remove(sale);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0;
        }

        public async Task<PaginatedResult<Sale>> GetAllAsync(PaginationParameters parameters, CancellationToken cancellationToken)
        {
            var query = _context.Sales.Include(s => s.Items).AsQueryable();

            query = ApplyFilters(query, parameters);

            query = ApplyOrdering(query, parameters.OrderBy);

            var totalItems = await query.CountAsync(cancellationToken);

            // Apply Pagination
            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Sale>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = parameters.Page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)parameters.PageSize)
            };
        }

        private IQueryable<Sale> ApplyFilters(IQueryable<Sale> query, PaginationParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.CustomerName))
            {
                query = query.Where(s => s.Customer.Contains(parameters.CustomerName));
            }

            if (parameters.MinTotal.HasValue)
            {
                query = query.Where(s => s.TotalSaleAmount >= parameters.MinTotal.Value);
            }

            if (parameters.MaxTotal.HasValue)
            {
                query = query.Where(s => s.TotalSaleAmount <= parameters.MaxTotal.Value);
            }

            if (parameters.MinDate.HasValue)
            {
                query = query.Where(s => s.SaleDate >= parameters.MinDate.Value);
            }

            if (parameters.MaxDate.HasValue)
            {
                query = query.Where(s => s.SaleDate <= parameters.MaxDate.Value);
            }

            return query;
        }

        private IQueryable<Sale> ApplyOrdering(IQueryable<Sale> query, string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return query.OrderByDescending(s => s.SaleDate); 
            }

            // Parse the ordering string
            var orderClauses = orderBy.Split(',');
            var ordered = false;

            foreach (var clause in orderClauses)
            {
                var trimmedClause = clause.Trim();
                var descending = trimmedClause.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
                var propertyName = descending ? trimmedClause[..^5].Trim() : trimmedClause;

                query = (propertyName.ToLower(), ordered) switch
                {
                    ("date", false) => descending ? query.OrderByDescending(s => s.SaleDate) : query.OrderBy(s => s.SaleDate),
                    ("date", true) => descending ? ((IOrderedQueryable<Sale>)query).ThenByDescending(s => s.SaleDate) : ((IOrderedQueryable<Sale>)query).ThenBy(s => s.SaleDate),
                    ("total", false) => descending ? query.OrderByDescending(s => s.TotalSaleAmount) : query.OrderBy(s => s.SaleDate),
                    ("total", true) => descending ? ((IOrderedQueryable<Sale>)query).ThenByDescending(s => s.SaleDate) : ((IOrderedQueryable<Sale>)query).ThenBy(s => s.TotalSaleAmount),
                    ("customername", false) => descending ? query.OrderByDescending(s => s.Customer) : query.OrderBy(s => s.Customer),
                    ("customername", true) => descending ? ((IOrderedQueryable<Sale>)query).ThenByDescending(s => s.Customer) : ((IOrderedQueryable<Sale>)query).ThenBy(s => s.Customer),
                    _ => query
                };

                ordered = true;
            }

            return query;
        }

        public async Task<Sale> GetBySaleNumberAsync(Guid saleNumber, CancellationToken cancellationToken)
        {

            var sale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber);

            if (sale == null) throw new BadHttpRequestException($"Sale with SaleNumber {saleNumber} not found.");

            sale.RecalculateSaleTotal();
            _context.SaveChangesAsync();
            return sale;

        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            var existingSale = await _context.Sales
                .FirstOrDefaultAsync(s => s.SaleNumber == sale.SaleNumber, cancellationToken);

            _context.Entry(existingSale).State = EntityState.Detached;

            _context.Sales.Attach(sale);
            _context.Entry(sale).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

    }
}