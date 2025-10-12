using Macone.Areas.User.ViewModels;
using Macone.Data;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    public class ShopDetailsController : Controller
    {
        private readonly AppDbContext _db;

        public ShopDetailsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductDetail(string id)
        {
            var sanPham = _db.Products.SingleOrDefault(x => x.Id == id);
            var anhSanPhams = _db.Images.Where(x => x.ProductId == id).ToList();
            if (sanPham == null)
            {
                return RedirectToAction("Index", "Shop");
            }
            var sanPhamViewModel = new ProductDetailViewModel(sanPham, anhSanPhams);
            return View(sanPhamViewModel);
        }

        public IActionResult RandomProduct()
        {
            var products = _db.Products.OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            return View(products);
        }
    }
}
