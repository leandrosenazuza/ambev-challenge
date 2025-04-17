using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.CountAsync(cancellationToken);
    }

    public async Task<Product?> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {

        product.Id = 0;

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Product?>> GetAllAsync(
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }


    public async Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken)
    {
        return await _context.Products
            .FirstOrDefaultAsync(u => u.Title == title, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(
        string category,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Category == category)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {

        var existingProduct = await _context.Products.FindAsync(new object[] { product.Id }, cancellationToken);

        if (existingProduct == null)
            throw new InvalidOperationException($"Product with ID {product.Id} not found.");

        _context.Entry(existingProduct).State = EntityState.Detached;

        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }

}