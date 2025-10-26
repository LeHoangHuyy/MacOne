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
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;

            var products = _db.Products.AsNoTracking().OrderByDescending(x => x.CreatedAt);

            PagedList<Product> lst = new PagedList<Product>(products, pageNumber, pageSize);


            return View(lst);
        }



        // Create Product
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories.ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, List<IFormFile> ImageFiles)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin-assets/img/products");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            bool isFirst = true;
            foreach (var file in ImageFiles)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var image = new Image
                    {
                        ImageFileName = fileName,
                        IsMain = isFirst,
                        CreatedAt = DateTime.Now,
                        ProductId = product.Id
                    };
                    isFirst = false;

                    _db.Images.Add(image);
                }
                
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // Update Product
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(_db.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.Now;
                _db.Products.Update(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_db.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }


        // Delete Product
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var prod = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (prod == null)
            {
                return NotFound();
            }
            _db.Products.Remove(prod);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Details Product
        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _db.Products.Include(p => p.Category).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Details(Product product)
        {
            return RedirectToAction("Index");
        }
    }
}
