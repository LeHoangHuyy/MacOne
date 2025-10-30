using Macone.Areas.Admin.Repositories;
using Macone.Areas.Admin.Services;
using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.Admin.Controllers.Api
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryApiController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/Admin/CategoryApi
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());


        // GET: api/Admin/CategoryApi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        // POST: api/Admin/CategoryApi
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Category category)
        {
            await _service.CreateAsync(category);
            return Ok(new { message = "Created successfully" });
        }

        // PUT: api/Admin/CategoryApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            await _service.UpdateAsync(id, category);
            return Ok(new { message = "Updated successfully!" });
        }

        // DELETE: api/Admin/CategoryApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
