using AutoMapper;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.Helpers.Exceptions.Company;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Enums;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Controllers
{
    [Authorize(Roles ="Company")]
    public class CompanyController : Controller
    {
        readonly IIndustryService _industryService;
        readonly ICompanyService _companyService;
        readonly IUserService _userService;
        readonly IMapper _mapper;

		public CompanyController(IIndustryService industryService, ICompanyService companyService, IUserService userService, IMapper mapper)
		{
			_industryService = industryService;
			_companyService = companyService;
			_userService = userService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Create()
        {
            ViewBag.Industries= await _industryService.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyDto dto, string stripeEmail, string stripeToken)
        {
            ViewBag.Industries =await  _industryService.GetAll();
            var user=await _userService.GetCurrentUser(User);
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _companyService.Create(dto,user.Id,stripeEmail,stripeToken);
                return RedirectToAction("Index", "Home");
            }
            catch (CompanyNameExistsException ex)
            {
				ModelState.AddModelError("CompanyName", ex.Message);
				return View(dto);
			}
            catch(CompanyLogoException ex) 
            {
                ModelState.AddModelError("Logo", ex.Message);
                return View(dto);
            }
            catch(CompanyIndustryException ex)
            {
				ModelState.AddModelError("IndustryIds", ex.Message);
				return View(dto);
			}
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            var company = await _companyService.GetById(user.CompanyId.Value,"Jobs", "IndustryCompanies", "IndustryCompanies.Industry");
            return View(company);
        }
        public async Task<IActionResult> Update(int id)
        {
			ViewBag.Industries = await _industryService.GetAll();
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId!=id)
            {
                return RedirectToAction("Profile", "Company");
            }
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            try
            {
                var company = await _companyService.GetById(id);
                var dto = _mapper.Map<UpdateCompanyDto>(company);
                return View(dto);
            }
            catch(CompanyNotFounException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCompanyDto dto)
        {
			ViewBag.Industries = await _industryService.GetAll();
            var user = await _userService.GetCurrentUser(User);
            if (!ModelState.IsValid)
			{
				return View(dto);
			}
            try
            {
                await _companyService.Update(dto, user.Id);
                return RedirectToAction("Profile");
            }
            catch (CompanyLogoException ex)
            {
				ModelState.AddModelError("Logo", ex.Message);
				return View(dto);
			}
			catch (CompanyIndustryException ex)
			{
				ModelState.AddModelError("IndustryIds", ex.Message);
				return View(dto);
			}
			catch (CompanyNameExistsException ex)
			{
				ModelState.AddModelError("CompanyName", ex.Message);
				return View(dto);
			}
            catch(CompanyNotFounException ex)
            {
                return RedirectToAction("Create");
            }
            catch(Exception ex)
            {
				ModelState.AddModelError("", ex.Message);
				return View(dto);
			}
		}
	}
}
