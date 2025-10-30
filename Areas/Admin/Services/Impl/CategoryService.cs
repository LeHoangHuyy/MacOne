using Macone.Areas.Admin.Repositories;
using Macone.Models.Entities;

namespace Macone.Areas.Admin.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.Now;
            await _repo.CreateAsync(category);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                throw new Exception("Category not found");
            }

            await _repo.DeleteAsync(existing);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task UpdateAsync(int id, Category category)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                throw new Exception("Category not found");
            }

            existing.CreatedAt = DateTime.Now;
            existing.Name = category.Name;

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
        }
    }
}
