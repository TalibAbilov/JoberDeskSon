using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _service;
        readonly IMapper _mapper;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data=await _service.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _service.Create(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (CategoryNameExsistException ex)
            {
                ModelState.AddModelError("Name",ex.Message);
                return View(dto); 
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var category=await _service.GetById(id);
                return View(_mapper.Map<UpdateCategoryDto>(category));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult>Update(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _service.Update(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(CategoryNameExsistException ex) 
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(dto);
            }
        }
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                 await _service.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
