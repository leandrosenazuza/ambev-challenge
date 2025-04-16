
using Ambev.DeveloperEvaluation.Domain.Entities;
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
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid saleNumber, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Sales.ToListAsync(cancellationToken);
        }

        public async Task<Sale> GetBySaleNumberAsync(Guid saleNumber, CancellationToken cancellationToken)
        {
            return await _context.Sales
                       .FirstOrDefaultAsync(u => u.SaleNumber == saleNumber, cancellationToken);
        }
    }
}
