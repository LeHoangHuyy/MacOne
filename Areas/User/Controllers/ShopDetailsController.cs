using Macone.Areas.User.Services;
using Microsoft.AspNetCore.Mvc;


namespace Macone.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/ShopDetails")]

    public class ShopDetailsController : Controller
    {
        private readonly IShopDetailsService _service;

        public ShopDetailsController(IShopDetailsService service)
        {
            _service = service;
        }

        [Route("{id}")]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            var viewModel = await _service.GetProductDetailsAsync(id.Value);

            if (viewModel == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            return View(viewModel);
        }
    }
}
