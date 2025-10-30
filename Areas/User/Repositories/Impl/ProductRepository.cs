using Macone.Data;
using Macone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.User.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetHomeProductsAsync(int? categoryId)
        {
            var query = _db.Products.Include(p => p.Images).AsNoTracking();

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            return await query.OrderByDescending(p => p.CreatedAt).Take(8).ToListAsync();
        }

        public async Task<(int total, List<Product> products)> GetShopProductsAsync(int? categoryId, int page, int pageSize)
        {
            var query = _db.Products.Include(p => p.Images).AsSingleQuery();

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            var total = await query.CountAsync();

            var products = await query.OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (total, products);
        }
    }
}
