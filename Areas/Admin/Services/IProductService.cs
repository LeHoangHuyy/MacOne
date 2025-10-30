using Macone.Models.DTOs;
using Macone.Models.Entities;

namespace Macone.Areas.Admin.Services
{
    public interface IProductService
    {
        Task<(int total, List<Product> products)> GetPagedAsync(int page, int pageSize);
        Task<Product?> GetByIdAsync(int id);
        Task CreateAsync(ProductDTO dto, string uploadPath);
        Task UpdateAsync(int id, ProductDTO dto, string uploadPath);
        Task DeleteAsync(int id, string uploadPath);
    }
}
