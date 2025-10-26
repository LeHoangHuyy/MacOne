using Macone.Areas.User.ViewModels;
using Macone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/ShopDetails")]

    public class ShopDetailsController : Controller
    {
        private readonly AppDbContext _db;

        public ShopDetailsController(AppDbContext db)
        {
            _db = db;
        }

        [Route("{id}")]
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            var product = _db.Products.Include(p => p.Images).FirstOrDefault(x => x.Id == id);
            var productImg = _db.Images.Where(x => x.ProductId == id).ToList();

            if (product == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            var sanPhamViewModel = new ProductDetailsViewModel(product, productImg);

            return View(sanPhamViewModel);
        }
    }
}
