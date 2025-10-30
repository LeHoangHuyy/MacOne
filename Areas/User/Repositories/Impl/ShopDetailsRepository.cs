using Macone.Data;
using Macone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.User.Repositories.Impl
{
    public class ShopDetailsRepository : IShopDetailsRepository
    {
        private readonly AppDbContext _db;

        public ShopDetailsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _db.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Image>> GetProductImagesAsync(int id)
        {
            return await _db.Images
                .Where(i => i.ProductId == id)
                .ToListAsync();
        }
    }
}
