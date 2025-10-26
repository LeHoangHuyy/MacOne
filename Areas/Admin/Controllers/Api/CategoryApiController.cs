using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.Admin.Controllers.Api
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoryApiController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/Admin/CategoryApi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var catrgories = await _db.Categories
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new {
                    c.Id,
                    c.Name,
                    c.CreatedAt
                })
                .ToListAsync();

            return Ok(catrgories);
        }

        // GET: api/Admin/CategoryApi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Admin/CategoryApi
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Category category)
        {
            category.CreatedAt = DateTime.Now;
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Created successfully" });
        }

        // PUT: api/Admin/CategoryApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Category updated)
        {
            if (id != updated.Id)
            {
                return BadRequest("ID mismatch");
            }

            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updated.Name;
            category.CreatedAt = DateTime.Now;

            await _db.SaveChangesAsync();
            return Ok(new { message = "Updated successfully!" });
        }

        // DELETE: api/Admin/CategoryApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
