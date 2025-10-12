using Macone.Data;
using Macone.Models.Entities;

namespace Macone.Repositories.impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            _context.Loais.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Loais;
        }

        public Category GetById(string id)
        {
            return _context.Loais.Find(id);
        }

        public Category Update(Category category)
        {
            _context.Loais.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
