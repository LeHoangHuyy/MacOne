using Macone.Data;
using Macone.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
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
        public IActionResult Register(User user)
        {
            if (_db.Users.Any(u => u.Username == user.Username))
            {
                ViewBag.Register = "Tên đăng nhập đã tồn tại!";
                return View("Login");
            }

            user.Role = "USER";
            _db.Users.Add(user);
            _db.SaveChanges();

            TempData["Success"] = "Đăng ký thành công! Hãy đăng nhập";
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
