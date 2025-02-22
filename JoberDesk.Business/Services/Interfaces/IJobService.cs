using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Business.Helpers.Enums;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
	public interface IJobService
	{
		Task<GetJobDto> GetById(int id, params string[] includes);
		Task<List<GetJobDto>> GetAll(params string[] includes);
		Task Create(CreateJobDto dto, string stripeEmail, string stripeToken);
		Task Update(UpdateJobDto dto);	
		Task Delete(int id);
		Task SoftDelete(int id);
		Task ReActivate(int id, string stripeEmail, string stripeToken);
		Task<List<GetJobDto>> FindAll(Expression<Func<Job, bool>> expression, params string[] includes);
        Task MakeJobPremium(int id, string stripeEmail, string stripeToken);
		Task CheckAndUpdatePremiumJobs();
		Task MarkOldJobsAsDeleted();

    }
}
