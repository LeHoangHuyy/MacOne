using Macone.Areas.Admin.Repositories.Base;
using Macone.Models.Entities;

namespace Macone.Areas.Admin.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllAsync();
    }
}
