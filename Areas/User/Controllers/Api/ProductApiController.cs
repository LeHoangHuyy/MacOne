using Macone.Areas.User.Services;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.Controllers.Api
{
    [Area("User")]
    [Route("api/user/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductApiController(IProductService service)
        {
            _service = service;
        }

        // GET: api/User/Home
        [HttpGet("home")]
        public async Task<IActionResult> GetHomeProducts(int? categoryId)
        {
            var products = await _service.GetHomeProductsAsync(categoryId);
            return Ok(products);
        }

        // GET: api/User/Shop
        [HttpGet("shop")]
        public async Task<IActionResult> GetShopProducts(int? categoryId, int page = 1, int pageSize = 9)
        {
            var result =  await _service.GetShopProductsAsynce(categoryId, page, pageSize);
            return Ok(result);
        }
    }
}
