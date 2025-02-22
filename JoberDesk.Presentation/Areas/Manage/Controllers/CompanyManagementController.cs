using AutoMapper;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.Helpers.Email;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JoberDesk.Presentation.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CompanyManagementController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public CompanyManagementController(ICompanyService companyService, IMapper mapper, IMailService mailService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index(string filter = "all")
        {
            List<GetCompanyDto> companies;

            switch (filter)
            {
                case "confirmed":
                    companies = await _companyService.FindAll(x => x.IsConfirmedByAdmin, "AppUser", "IndustryCompanies", "IndustryCompanies.Industry");
                    break;
                case "unconfirmed":
                    companies = await _companyService.FindAll(x => !x.IsConfirmedByAdmin, "AppUser", "IndustryCompanies", "IndustryCompanies.Industry");
                    break;
                default:
                    companies = await _companyService.GetAll("AppUser", "IndustryCompanies", "IndustryCompanies.Industry");
                    break;
            }

            ViewBag.SelectedFilter = filter;
            return View(companies);
        }
        public async Task<IActionResult> CompanyDetail(int id)
        {
            try
            {
                var company=await _companyService.GetById(id, "AppUser", "IndustryCompanies", "IndustryCompanies.Industry");
                return View(company);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> ConfirmCompany(int id)
        {
            try
            {

                var company =await  _companyService.GetById(id,"IndustryCompanies", "IndustryCompanies.Industry");
                company.IsConfirmedByAdmin = true;
                UpdateCompanyDto dto = _mapper.Map<UpdateCompanyDto>(company);
                dto.IndustryIds = company.IndustryCompanies.Select(x => x.IndustryId).ToList();
			    dto.LastRejectionReason = null;
                dto.LastRejectionTime = null;
			    await _companyService.Update(dto,company.AppUserId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> SendRejectionMessage(int id)
        {
            try
            {

                var company = await _companyService.GetById(id);

                var data= new SendRejectionMessageDto
                {
                    CompanyId = company.Id,
                };

                return View(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendRejectionMessage(SendRejectionMessageDto data)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                var company = await _companyService.GetById(data.CompanyId,"AppUser","IndustryCompanies", "IndustryCompanies.Industry");

                UpdateCompanyDto dto = _mapper.Map<UpdateCompanyDto>(company);
                dto.IsConfirmedByAdmin = false;
                dto.LastRejectionReason = data.LastRejectionReason;
                dto.LastRejectionTime = DateTime.Now;
                dto.IndustryIds = company.IndustryCompanies.Select(x => x.IndustryId).ToList();

                await _companyService.Update(dto,company.AppUserId);

                MailRequest request = new MailRequest()
                {
                
                    ToEmail = company.AppUser.Email,
                    Subject = "Şirkətiniz təsdiqlənməmə səbəbi",
                    Body = $"<p>Şirkətiniz təsdiqlənmədi.</p><p><strong>Səbəb:</strong> {data.LastRejectionReason}</p>Zəhmət olmasa şirkət məlumatlarını redaktə edin."
                };

                await _mailService.SendEmailAsync(request);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
