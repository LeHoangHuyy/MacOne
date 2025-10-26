using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Macone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
