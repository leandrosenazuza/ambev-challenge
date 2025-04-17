﻿using Ambev.DeveloperEvaluation.Domain.Entities;
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

            var totalItems = await query.CountAsync(cancellationToken);
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