using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    public class ShopDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
