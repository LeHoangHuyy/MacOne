using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Giỏ Hàng";
            return View();
        }
    }
}
