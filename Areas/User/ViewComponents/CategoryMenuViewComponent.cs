using Macone.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _repo;

        public CategoryMenuViewComponent(ICategoryRepository repository)
        {
            _repo = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _repo.GetAllAsync();
            var orderedCategories = categories.OrderBy(x => x.Name).ToList();
            return View("Menu", orderedCategories);
        }
    }
}
