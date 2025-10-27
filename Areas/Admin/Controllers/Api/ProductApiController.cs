using Macone.Data;
using Macone.Models.DTOs;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProductApiController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // GET: api/Admin/ProductApi
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var query = _db.Products
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt);

            var total = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.CategoryId,
                    p.Price,
                    p.Stock,
                    p.Weight,
                    p.CreatedAt
                })
                .ToListAsync();

            return Ok(new { total, products });
        }

        // GET: api/Admin/ProductApi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _db.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound(new { message = "Product not found"});
                
            return Ok(product);
        }

        // POST: api/Admin/ProductApi
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Stock = model.Stock,
                Weight = model.Weight,
                Size = model.Size,
                Description = model.Description,
                Information = model.Information,
                CreatedAt = DateTime.Now
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin-assets/img/products");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            bool isFirst = true;
            foreach (var file in model.ImageFiles ?? new List<IFormFile>())
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

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

            await _db.SaveChangesAsync();

            return Ok(new { message = "Created successfully!" });
        }

        // PUT: api/Admin/ProductApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductDTO model)
        {
            var product = await _db.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound(new { message = "Product not found" });

            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.Size = model.Size;
            product.Weight = model.Weight;
            product.CategoryId = model.CategoryId;
            product.Information = model.Information;
            product.Description = model.Description;
            product.CreatedAt = DateTime.Now;

            if (model.ImageFiles != null && model.ImageFiles.Any())
            {
                string uploadDir = Path.Combine(_env.WebRootPath, "admin-assets", "img", "products");

                // Xoá ảnh cũ trong thư mục
                foreach (var img in product.Images)
                {
                    var oldPath = Path.Combine(uploadDir, img.ImageFileName ?? "");
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // Xoá ảnh cũ trong DB
                _db.Images.RemoveRange(product.Images);

                // Upload ảnh mới
                bool isFirst = true;
                foreach (var file in model.ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadDir, fileName);

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
            }

            await _db.SaveChangesAsync();
            return Ok(new { message = "Updated successfully!" });
        }

        // DELETE: api/Admin/ProductApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _db.Products
                    .AsNoTracking()
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    // Nếu không tìm thấy theo context, thử truy vấn lại database trực tiếp
                    bool exists = await _db.Products.AsNoTracking().AnyAsync(p => p.Id == id);
                    if (!exists)
                        return NotFound(new { message = "Product not found (checked twice)" });
                }

                string uploadDir = Path.Combine(_env.WebRootPath, "admin-assets", "img", "products");

                // Xóa ảnh vật lý (nếu có)
                if (product?.Images != null)
                {
                    foreach (var img in product.Images)
                    {
                        var filePath = Path.Combine(uploadDir, img.ImageFileName ?? "");
                        if (System.IO.File.Exists(filePath))
                        {
                            try { System.IO.File.Delete(filePath); } catch { /* ignore */ }
                        }
                    }

                    _db.Images.RemoveRange(product.Images);
                }

                // Xóa sản phẩm (có thể reattach nếu bị detach)
                if (product != null)
                {
                    _db.Products.Attach(product);
                    _db.Products.Remove(product);
                }
                else
                {
                    var stub = new Product { Id = id };
                    _db.Entry(stub).State = EntityState.Deleted;
                }

                await _db.SaveChangesAsync();

                return Ok(new { message = "Product deleted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting product: " + ex.Message });
            }
        }

    }
}
