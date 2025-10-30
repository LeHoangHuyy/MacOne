using Macone.Data;
using Macone.Models.Entities;
using Macone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _service.LoginAsync(username, password);
            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu !";
                return View();
            }

            // Save session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // Save cookie
            Response.Cookies.Append("Username", user.Username);
            Response.Cookies.Append("Role", user.Role);

            if (user.Role == "ADMIN")
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "User" });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var (success, message) = await _service.RegisterAsync(user);

            if (!success)
            {
                ViewBag.Error = message;
                return View("Login");
            }

            TempData["Success"] = message;
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Role");
            return RedirectToAction("Login");
        }
    }
}
