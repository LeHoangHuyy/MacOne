using Macone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.ViewComponents
{
    public class CategoryTabMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _repository;
        public CategoryTabMenuViewComponent(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _repository.GetAll().OrderBy(x => x.Name);
            return View("TabMenu", categories);
        }
    }
}
