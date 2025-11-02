using Macone.Data;
using Macone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.Admin.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Product product)
        {
            await _db.Products.AddAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await Task.CompletedTask;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products
                .Include(p => p.Images)        
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetPagedAsync(int page, int pageSize)
        {
            return await _db.Products
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalAsync() => await _db.Products.CountAsync();

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await Task.CompletedTask;
        }
    }
}
