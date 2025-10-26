using Macone.Data;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public IActionResult Index(int? id)
        {
            return View();
        }
    }
}
