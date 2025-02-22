using JoberDesk.Business.Services.Interfaces;
using JoberDesk.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Controllers
{
    public class IndustryController : Controller
    {

        readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {

           
            _industryService = industryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var industries = await _industryService.GetAll("IndustryCompanies");
                return View(industries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
