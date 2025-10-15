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

        public IActionResult Index(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Shop");
            }

            var sanPham = _db.Products.SingleOrDefault(x => x.Id == id);
            var anhSanPhams = _db.Images.Where(x => x.ProductId == id).ToList();

            if (sanPham == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            var sanPhamViewModel = new ProductDetailsViewModel(sanPham, anhSanPhams);

            ViewData["Title"] = "Chi tiết sản phẩm";

            return View(sanPhamViewModel);
        }

        public IActionResult RandomProduct()
        {
            var products = _db.Products.OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            return View(products);
        }
    }
}
