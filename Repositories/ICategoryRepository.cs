using Macone.Models.Entities;

namespace Macone.Repositories
{
    public interface ICategoryRepository
    {
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(string id);
        Category GetById(string id);
        IEnumerable<Category> GetAll();

    }
}
