using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.DTOs.Home;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JoberDesk.Presentation.Controllers
{
	public class HomeController : Controller
	{

		private readonly IJobService _jobService;
		private readonly ILogger<HomeController> _logger;
		private readonly ICompanyService _companyService;
		private readonly ICategoryService _categoryService;

		public HomeController(ILogger<HomeController> logger, IJobService jobService, ICompanyService companyService, ICategoryService categoryService)
		{
			_logger = logger;
			_jobService = jobService;
			_companyService = companyService;
			_categoryService = categoryService;
		}



		public async Task<IActionResult> Index(string search, int? categoryId, string jobType, string educationLevel, string experienceYear, string sortBy, int minSalary = 0, int maxSalary = 10000,int page=1, int pageSize = 3)
		{
			try
			{
				var categories = await _categoryService.GetAll();
				ViewBag.Categories = categories;

				var jobs = await _jobService.FindAll(x => x.EndTime > DateTime.Now && x.Company.IsConfirmedByAdmin == true && x.IsDeleted==false, "Company", "Category");
				var vacancyCount = jobs.Count;

				if (categoryId.HasValue)
				{
					jobs = jobs.Where(x=>x.CategoryId==categoryId.Value).ToList();
				}

				if (!string.IsNullOrEmpty(search))
				{
					jobs = jobs.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
				}

				if (!string.IsNullOrEmpty(jobType))
				{
					jobs = jobs.Where(x => x.EmploymentType.ToString() == jobType).ToList();
				}

				if (!string.IsNullOrEmpty(educationLevel))
				{
					jobs = jobs.Where(x => x.EducationLevel.ToString() == educationLevel).ToList();
				}

				if (!string.IsNullOrEmpty(experienceYear))
				{
					jobs = jobs.Where(x => x.ExperienceLevel.ToString() == experienceYear).ToList();
				}

				jobs = jobs.Where(x => x.Salary >= minSalary && x.Salary <= maxSalary).ToList();


				if (sortBy == "newest")
				{
					jobs = jobs.OrderByDescending(x => x.CreatedAt).ToList();
				}
				else if (sortBy == "oldest")
				{
					jobs = jobs.OrderBy(x => x.CreatedAt).ToList();
				}

				jobs = jobs.OrderByDescending(x => x.IsPremium).ToList();

				int totalJobs = jobs.Count;
				var paginatedJobs = jobs.Skip((page - 1) * pageSize).Take(pageSize).ToList();
				int totalPages = (int)Math.Ceiling((double)totalJobs / pageSize);



				var dto = new JobPaginationDto
				{
					Jobs = paginatedJobs,
					TotalJobs = totalJobs,
					CurrentPage = page,
					PageSize = pageSize,
					TotalPages = totalPages
				};

				var companies = await _companyService.FindAll(x => x.IsConfirmedByAdmin == true);

				HomeDto homeDto = new HomeDto
				{
					JobPaginationDto = dto,
					CompanyCount = companies.Count,
					JobCount = vacancyCount,
					Categories = categories
				};
				return View(homeDto);
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		public IActionResult Services()
		{
			return View();
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
