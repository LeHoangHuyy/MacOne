namespace Macone.Areas.User.Services
{
    public interface IProductService
    {
        Task<List<object>> GetHomeProductsAsync(int? categoryId);
        Task<object> GetShopProductsAsynce(int? categoryId, int page, int pageSize);
    }
}
