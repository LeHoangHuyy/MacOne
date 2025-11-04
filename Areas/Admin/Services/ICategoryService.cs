using Macone.Areas.Admin.Repositories;
using Macone.Models.DTOs;
using Macone.Models.Entities;

namespace Macone.Areas.Admin.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(CategoryDTO dto);
        Task UpdateAsync(int id, CategoryDTO dto);
        Task DeleteAsync(int id);
    }
}
