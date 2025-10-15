using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    public class ShopController : Controller
    {
        private readonly AppDbContext _db;

        public ShopController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? id, int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;

            var query = _db.Products.Include(p => p.Images).AsNoTracking();

            if (id != null)
            {
                query = query.Where(x => x.CategoryId == id);
            }

            var products = query.OrderByDescending(x => x.CreatedAt);

            PagedList<Product> lst = new PagedList<Product>(products, pageNumber, pageSize);

            ViewData["Title"] = "Cửa hàng";

            return View(lst);
        }
    }
}
