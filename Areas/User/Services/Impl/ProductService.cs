using Macone.Areas.User.Repositories;

namespace Macone.Areas.User.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<object>> GetHomeProductsAsync(int? categoryId)
        {
            var products = await _repo.GetHomeProductsAsync(categoryId);
            return products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                Image = p.Images.FirstOrDefault(i => i.IsMain)?.ImageFileName
            } as object).ToList();
        }

        public async Task<object> GetShopProductsAsynce(int? categoryId, int page, int pageSize)
        {
            var (total, products) = await _repo.GetShopProductsAsync(categoryId, page, pageSize);
            var mapped = products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                Image = p.Images.FirstOrDefault(i => i.IsMain)?.ImageFileName
            } as object).ToList();

            return new {total, products = mapped};
        }
    }
}
