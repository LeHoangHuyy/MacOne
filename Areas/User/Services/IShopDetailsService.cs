using Macone.Areas.User.ViewModels;

namespace Macone.Areas.User.Services
{
    public interface IShopDetailsService
    {
        Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id);
    }
}
