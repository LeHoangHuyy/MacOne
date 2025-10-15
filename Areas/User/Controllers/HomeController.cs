using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index(int? id)
        {
            var query = _db.Products.Include(p => p.Images).AsNoTracking();
            
            if (id != null)
            {
                query = query.Where(x => x.CategoryId == id);
            }

            var products = query.OrderByDescending(x => x.CreatedAt).Take(8).ToList();

            ViewData["Title"] = "Trang chủ";

            return View(products);
        }
    }
}
