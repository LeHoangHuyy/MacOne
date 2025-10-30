using Macone.Models.Entities;

namespace Macone.Areas.User.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetHomeProductsAsync(int? categoryId);
        Task<(int total, List<Product> products)> GetShopProductsAsync(int? categoryId, int page, int pageSize);
    }
}
