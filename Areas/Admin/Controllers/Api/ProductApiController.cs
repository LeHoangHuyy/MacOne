using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductApiController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/Admin/ProductApi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _db.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Stock,
                    p.CategoryId,
                    p.CreatedAt
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Admin/ProductApi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _db.Products.Include(p => p.Category)
                                            .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Admin/ProductApi
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            product.CreatedAt = DateTime.Now;
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Created successfully!" });
        }

        // PUT: api/Admin/ProductApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updated)
        {
            if (id != updated.Id)
                return BadRequest("ID mismatch");

            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;
            product.Size = updated.Size;
            product.Weight = updated.Weight;
            product.CategoryId = updated.CategoryId;

            await _db.SaveChangesAsync();
            return Ok(new { message = "Updated successfully!" });
        }

        // DELETE: api/Admin/ProductApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Deleted successfully!" });
        }
    }
}
