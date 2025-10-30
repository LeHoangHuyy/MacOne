using Macone.Areas.User.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Macone.Areas.User.Controllers.Api
{
    [Area("User")]
    [Route("api/User/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductApiController(IProductService service)
        {
            _service = service;
        }

        // GET: api/User/Home
        [HttpGet("Home")]
        public async Task<IActionResult> GetHomeProducts(int? categoryId)
        {
            var products = await _service.GetHomeProductsAsync(categoryId);
            return Ok(products);
        }

        // GET: api/User/Shop
        [HttpGet("Shop")]
        public async Task<IActionResult> GetShopProducts(int? categoryId, int page = 1, int pageSize = 9)
        {
            var result =  await _service.GetShopProductsAsynce(categoryId, page, pageSize);
            return Ok(result);
        }
    }
}
