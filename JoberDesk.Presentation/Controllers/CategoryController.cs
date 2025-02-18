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
            var categories = await _categoryService.GetAll("Jobs");
            return View(categories);
        }
    }
}

