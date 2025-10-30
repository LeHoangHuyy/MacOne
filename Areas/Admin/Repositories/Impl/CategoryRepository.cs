using Macone.Data;
using Macone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.Admin.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Category category) => await _db.Categories.AddAsync(category);

        public Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            return Task.CompletedTask;
        }

        public async Task<List<Category>> GetAllAsync() => await _db.Categories.OrderByDescending(c => c.CreatedAt).ToListAsync();

        public Task<Category?> GetByIdAsync(int id) => _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            await Task.CompletedTask;
        }
    }
}
