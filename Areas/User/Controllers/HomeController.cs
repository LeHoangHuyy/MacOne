using Macone.Data;
using Microsoft.AspNetCore.Mvc;

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
            var listSanPham = _db.SanPhams.ToList();
            return View(listSanPham);
        }
    }
}
