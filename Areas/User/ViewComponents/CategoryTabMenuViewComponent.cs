using Macone.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.ViewComponents
{
    public class CategoryTabMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _repo;
        public CategoryTabMenuViewComponent(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _repo.GetAllAsync();
            var orderedCategories = categories.OrderBy(x => x.Name).ToList();
            return View("TabMenu", orderedCategories);
        }
    }
}
