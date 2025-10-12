using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var listSanPham = _db.Products.Take(8).ToList();
            return View(listSanPham);
        }

        public IActionResult ProductClassification(string id)
        {
            var listSanPham = _db.Products
                .Where(x => x.CategoryId == id)
                .Take(8)
                .ToList();
            return View(listSanPham);
        }
    }
}
