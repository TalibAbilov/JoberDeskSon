using AutoMapper;
using JoberDesk.Business.DTOs.Setting;
using JoberDesk.Business.Helpers.Exceptions.Setting;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace JoberDesk.Presentation.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles ="Admin")]
	public class SettingController : Controller
	{
		readonly ILayoutService _layoutService;
		readonly IMapper _mapper;

		public SettingController(ILayoutService layoutService, IMapper mapper)
		{
			_layoutService = layoutService;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var settings =_layoutService.GetSetting();

			return View(settings);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateSettingDto dto)
		{
			if (!ModelState.IsValid)
			{
				return View(dto);
			}
			try
			{
				await _layoutService.Create(dto);
				return RedirectToAction(nameof(Index));

			}
			catch(SettingExsistException ex)
			{
				ModelState.AddModelError("Key",ex.Message);
				return View(dto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		public async Task<IActionResult> Delete(string key)
		{
			try
			{
				await _layoutService.Delete(key);
			return RedirectToAction(nameof(Index));
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		public async Task<IActionResult> Update(string key)
		{
			var setting = await _layoutService.GetByKey(key);
			if (setting == null)
			{
				return NotFound();
			}

			var dto=_mapper.Map<UpdateSettingDto>(setting);
		
			return View(dto);
		}
		[HttpPost]
		public async Task<IActionResult> Update(UpdateSettingDto dto)
		{
			if (!ModelState.IsValid)
			{
				return View(dto); 
			}

			try
			{
				await _layoutService.Update(dto); 
				return RedirectToAction("Index"); 
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(dto);
			}
		}
	}
}
