using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Controllers
{
	public class CompaniesController : Controller
	{
		private readonly ICompanyService _companyService;
		private readonly IIndustryService _industryService;

        public CompaniesController(ICompanyService companyService, IIndustryService industryService)
        {
            _companyService = companyService;
            _industryService = industryService;
        }

        public async Task<IActionResult> Index(string search, int? industryId, int page = 1,int pageSize=3)
		{


            try
            {

			    ViewBag.Industries=await _industryService.GetAll();
                ViewBag.Search = search;
                var companies=await _companyService.FindAll(x=>x.IsConfirmedByAdmin==true,"Jobs","IndustryCompanies", "IndustryCompanies.Industry");

                if (!string.IsNullOrEmpty(search))
                {
                    companies=companies.Where(c => c.CompanyName.ToLower().Contains(search.ToLower())).ToList();
                }

                if (industryId is not null)
                {
                    companies=companies.Where(c => c.IndustryCompanies.Any(x=>x.Industry.Id == industryId)).ToList();
                }

                int totalCompanies=companies.Count;
                var paginatedCompanies=companies.Skip((page-1)*pageSize).Take(pageSize).ToList();
                int totalPages = (int)Math.Ceiling((double)totalCompanies/pageSize);



                var dto = new CompanyPaginationDto
                {
                    Companies = paginatedCompanies,
                    TotalCompanies = totalCompanies,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages=totalPages
                };

                return View(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<ActionResult> CompanyDetail(int id)
        {
            try
            {

                var company=await _companyService.GetById(id,"AppUser", "Jobs", "IndustryCompanies", "IndustryCompanies.Industry");
   
                return View(company);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

	}
}
