using Macone.Areas.Admin.Services;
using Macone.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _env;

        public ProductApiController(IProductService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        private string GetUploadPath()
        {
            return Path.Combine(_env.WebRootPath, "admin-assets", "img", "products");
        }

        // GET: api/Admin/ProductApi
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var (total, products) = await _service.GetPagedAsync(page, pageSize);

            return Ok(new { total, products });
        }

        // GET: api/Admin/ProductApi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Admin/ProductApi
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            await _service.CreateAsync(dto, GetUploadPath());
            return Ok(new { message = "Created successfully!" });
        }

        // PUT: api/Admin/ProductApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            await _service.UpdateAsync(id, dto, GetUploadPath());
            return Ok(new { message = "Updated successfully!" });
        }

        // DELETE: api/Admin/ProductApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id, GetUploadPath());
            return Ok(new { message = "Deleted successfully!" });
        }

    }
}
