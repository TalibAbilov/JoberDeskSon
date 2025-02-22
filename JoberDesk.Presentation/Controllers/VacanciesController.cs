using AutoMapper;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Controllers
{
    public class VacanciesController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public VacanciesController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        public async Task<IActionResult> VacancyDetail(int id)
        {
            try
            {
                var job = await _jobService.GetById(id, "Company", "Category");

                string viewedJobs = Request.Cookies["ViewedJobs"];

                if (string.IsNullOrEmpty(viewedJobs) || !viewedJobs.Contains($"|{id}|"))
                {
                    job.ViewCount += 1;

                    viewedJobs += $"|{id}|";
                    Response.Cookies.Append("ViewedJobs", viewedJobs, new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(5)
                    });

                    UpdateJobDto dto = _mapper.Map<UpdateJobDto>(job);
                    await _jobService.Update(dto);
                }
                return View(job);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
