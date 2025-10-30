using Macone.Models.Entities;

namespace Macone.Areas.User.Repositories
{
    public interface IShopDetailsRepository
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Image>> GetProductImagesAsync(int id);
    }
}
