using Macone.Data;
using Macone.Models.Entities;

namespace Macone.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

    }
}
