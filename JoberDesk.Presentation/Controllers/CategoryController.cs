using JoberDesk.Business.Services.Implementations;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoberDesk.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        readonly JoberDeskDbContext _context;
		readonly ICategoryService _categoryService;

        public CategoryController(JoberDeskDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Filter(string search)
        {
            var categories = await _categoryService.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return PartialView("_CategoryListPartial", categories);
        }
    }
}

