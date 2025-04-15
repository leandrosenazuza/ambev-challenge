
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        Task<Sale> ISaleRepository.AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<bool> ISaleRepository.DeleteAsync(Guid saleNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Sale>> ISaleRepository.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Sale> ISaleRepository.GetByIdAsync(Guid saleNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
