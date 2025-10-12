using Macone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Macone.Areas.User.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _repository;

        public CategoryMenuViewComponent(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _repository.GetAll().OrderBy(x => x.TenLoai);
            return View("Menu", categories);
        }
    }
}
