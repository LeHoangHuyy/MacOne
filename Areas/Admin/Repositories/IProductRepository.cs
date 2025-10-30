using Macone.Areas.Admin.Repositories.Base;
using Macone.Models.Entities;

namespace Macone.Areas.Admin.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<int> GetTotalAsync();
        Task<List<Product>> GetPagedAsync(int page, int pageSize);
    }
}
