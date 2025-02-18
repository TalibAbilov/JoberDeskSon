using AutoMapper;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Business.Helpers.Enums;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Helpers.Exceptions.Company;
using JoberDesk.Business.Helpers.Exceptions.Job;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JoberDesk.Presentation.Controllers
{
	[Authorize(Roles ="Company")]
	public class JobController : Controller
	{
		readonly ICategoryService _categoryService;
		readonly IUserService _userService;
		readonly IJobService _jobService;
		readonly ICompanyService _companyService;
        readonly IMapper _mapper;
        public JobController(ICategoryService categoryService, IUserService userService, IJobService jobService, ICompanyService companyService, IMapper mapper)
        {
            _categoryService = categoryService;
            _userService = userService;
            _jobService = jobService;
            _companyService = companyService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Detail(int id)
		{
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            var company=await _companyService.GetById(user.CompanyId.Value,"Jobs");
            
            try
            {
				var job = await _jobService.GetById(id,"Category");
                if (company.Jobs.Any(x => x.Id == job.Id)){

				    return View(job);
                }
                else
                {
                    return BadRequest();
                }
			}
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            
		}
        public async Task<IActionResult> MyVacancies()
		{
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
			var vacancies=await _jobService.FindAll(x=>x.CompanyId == user.CompanyId.Value,"Category");
			return View(vacancies);
		}
		public async Task<IActionResult> Create()
		{
			ViewBag.Categories = await _categoryService.GetAll();
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            return View();
		}
		[HttpPost]
		public async Task<IActionResult>Create(CreateJobDto dto, string stripeEmail, string stripeToken)
		{
            ViewBag.Categories = await _categoryService.GetAll();
            var user = await _userService.GetCurrentUser(User);


            if (!ModelState.IsValid)
			{
				return View(dto);
			}
                dto.CreatedAt = DateTime.Now;
                dto.CompanyId = user.CompanyId.Value;
			try
			{
				await _jobService.Create(dto,stripeEmail,stripeToken);
				return RedirectToAction(nameof(MyVacancies));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        public async Task<IActionResult>Update(int id)
        {
            ViewBag.Categories = await _categoryService.GetAll();
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            var company= await _companyService.GetById(user.CompanyId.Value,"Jobs");
            if (company == null)
            {
                throw new Exception("Sirket tapilmadi");
            }
            if (!company.Jobs.Any(job => job.Id == id))
            {
                return BadRequest();
            }
            try
            {
                var job = await _jobService.GetById(id);
                var dto = _mapper.Map<UpdateJobDto>(job);
                return View(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult>Update(UpdateJobDto dto)
        {
            ViewBag.Categories = await _categoryService.GetAll();
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                
                await _jobService.Update(dto);
                return RedirectToAction(nameof(MyVacancies));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
        public async Task<IActionResult>Delete(int id)
		{
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            var company = await _companyService.GetById(user.CompanyId.Value, "Jobs");
            if (company == null)
            {
                throw new Exception("Sirket tapilmadi");
            }
            if (!company.Jobs.Any(job => job.Id == id))
            {
                return RedirectToAction(nameof(MyVacancies));
            }
            try
            {
                await _jobService.Delete(id);
                return RedirectToAction(nameof(MyVacancies));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> MakePremium(int id)
        {
            var user = await _userService.GetCurrentUser(User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            var company = await _companyService.GetById(user.CompanyId.Value, "Jobs");
            if (company == null)
            {
                throw new Exception("Sirket tapilmadi");
            }
            if (!company.Jobs.Any(job => job.Id == id))
            {
                return RedirectToAction(nameof (MyVacancies));
            }
            try
            {
                var job=await _jobService.GetById(id);
             

                return View(job);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> MakePremium(int id,string stripeEmail,string stripeToken)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _jobService.MakeJobPremium(id, stripeEmail, stripeToken);
                return RedirectToAction(nameof(MyVacancies));
            }
            catch (JobAlreadyPremiumException ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
            catch(PremiumEndGreaterThanEndTime ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
