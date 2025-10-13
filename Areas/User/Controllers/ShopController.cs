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

        public IActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;

            var listSanPham = _db.Products.AsNoTracking().OrderBy(x => x.CreatedAt);

            PagedList<Product> lst = new PagedList<Product>(listSanPham, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult ProductClassification(string id, int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;

            var listSanPham = _db.Products
                .Where(x => x.CategoryId == id)
                .OrderBy(x => x.CreatedAt)
                .ToList();

            PagedList<Product> lst = new PagedList<Product>(listSanPham, pageNumber, pageSize);
            return View(lst);
        }
    }
}
