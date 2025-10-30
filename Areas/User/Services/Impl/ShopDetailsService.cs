using Macone.Areas.User.Repositories;
using Macone.Areas.User.ViewModels;

namespace Macone.Areas.User.Services.Impl
{
    public class ShopDetailsService : IShopDetailsService
    {
        private readonly IShopDetailsRepository _repo;

        public ShopDetailsService(IShopDetailsRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null) return null;

            var images = await _repo.GetProductImagesAsync(id);

            return new ProductDetailsViewModel(product, images);
        }
    }
}
