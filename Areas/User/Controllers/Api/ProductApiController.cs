using Macone.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.User.Controllers.Api
{
    [Area("User")]
    [Route("api/user/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("home")]
        public IActionResult GetHomeProducts(int? categoryId)
        {
            var query = _context.Products.Include(p => p.Images).AsNoTracking();

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            var products = query
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    Image = p.Images.FirstOrDefault(i => i.IsMain).ImageFileName
                })
                .ToList();
            return Ok(products);
        }

        [HttpGet("shop")]
        public IActionResult GetShopProducts(int? categoryId, int page = 1, int pageSize = 9)
        {
            var query = _context.Products.Include(p => p.Images).AsNoTracking();

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            var total = query.Count();

            var products = query.OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    Image = p.Images.FirstOrDefault(i => i.IsMain).ImageFileName
                }).ToList();

            return Ok(new {total, products});
        }
    }
}
