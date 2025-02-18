using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Helpers.Exceptions.Industry;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class IndustryController : Controller
    {
        readonly IIndustryService _service;
        readonly IMapper _mapper;

        public IndustryController(IIndustryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll("IndustryCompanies");
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateIndustryDto dto)
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
            catch (IndustryNameExsistException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(dto);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var industry = await _service.GetById(id);
                return View(_mapper.Map<UpdateIndustryDto>(industry));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateIndustryDto dto)
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
            catch (IndustryNameExsistException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(dto);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
