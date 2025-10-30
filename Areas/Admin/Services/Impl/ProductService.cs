using Macone.Areas.Admin.Repositories;
using Macone.Data;
using Macone.Models.DTOs;
using Macone.Models.Entities;

namespace Macone.Areas.Admin.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly AppDbContext _db;

        public ProductService(IProductRepository repo, AppDbContext db) 
        { 
            _repo = repo;
            _db = db;
        }

        private void DeleteOldImages(Product product, string uploadPath)
        {
            foreach (var img in product.Images)
            {
                var oldPath = Path.Combine(uploadPath, img.ImageFileName ?? "");
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }
            }

            _db.Images.RemoveRange(product.Images);
        }

        private async Task UploadProductImagesAsync(Product product, IEnumerable<IFormFile>? imageFiles, string uploadPath)
        {

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            bool isFirst = true;
            foreach (var file in imageFiles ?? new List<IFormFile>())
            {
                if (file.Length <= 0)
                {
                    continue;
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var image = new Image
                {
                    ImageFileName = fileName,
                    IsMain = isFirst,
                    CreatedAt = DateTime.Now,
                    ProductId = product.Id
                };

                isFirst = false;
                _db.Images.Add(image);
            }
        }

        public async Task CreateAsync(ProductDTO dto, string uploadPath)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Stock = dto.Stock,
                Weight = dto.Weight,
                Size = dto.Size,
                Description = dto.Description,
                Information = dto.Information,
                CreatedAt = DateTime.Now
            };

            await _repo.UpdateAsync(product);
            await _db.SaveChangesAsync();

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            await UploadProductImagesAsync(product, dto.ImageFiles, uploadPath);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string uploadPath)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            DeleteOldImages(product, uploadPath);

            await _repo.DeleteAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);


        public async Task<(int total, List<Product> products)> GetPagedAsync(int page, int pageSize)
        {
            var total = await _repo.GetTotalAsync();
            var products = await _repo.GetPagedAsync(page, pageSize);
            return (total, products);
        }

        public async Task UpdateAsync(int id, ProductDTO dto, string uploadPath)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null )
            {
                throw new Exception("Product not found");
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.Stock = dto.Stock;
            product.Size = dto.Size;
            product.Weight = dto.Weight;
            product.Information = dto.Information;
            product.Description = dto.Description;
            product.CreatedAt = DateTime.Now;

            if (dto.ImageFiles != null && dto.ImageFiles.Any())
            {
                DeleteOldImages(product, uploadPath);

                await UploadProductImagesAsync(product, dto.ImageFiles, uploadPath);
            }

            await _repo.SaveChangesAsync();
        }

    }
}
