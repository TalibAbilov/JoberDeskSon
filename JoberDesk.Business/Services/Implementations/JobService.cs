using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Business.Helpers.Enums;
using JoberDesk.Business.Helpers.Exceptions.Base;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Helpers.Exceptions.Company;
using JoberDesk.Business.Helpers.Exceptions.Job;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using JoberDesk.DAL.Repositories.Implementations;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JoberDesk.Business.Services.Implementations
{
    public class JobService : IJobService
	{
		readonly ICompanyService _companyService;
		readonly IJobRepository _rep;
		readonly IMapper _mapper;

		public JobService(IJobRepository rep, IMapper mapper, ICompanyService companyService)
		{
			_rep = rep;
			_mapper = mapper;
			_companyService = companyService;
		}

		public async Task Create(CreateJobDto dto, string stripeEmail, string stripeToken)
		{

			var company=await _companyService.GetById(dto.CompanyId.Value);
			if(company == null)
			{
				throw new CompanyNotFounException("Sirket tapilmadi");
			}
			var companyName=company.CompanyName;
            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = companyName,
                Phone = "+994 50 66"
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);

            var optionsCharge = new ChargeCreateOptions
            {

                Amount = (long)500,
                Currency = "USD",
                Description = "Product Selling amount",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status != "succeeded")
            {
                throw new Exception("Odenisde problem var");
            }

            var job = _mapper.Map<Job>(dto);
			await _rep.Create(job);
			await _rep.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			if (id <= 0)
			{
				throw new NegativeOrZeroIdException();
			}
			var job = await _rep.GetById(id);
			if (job == null)
			{
				    throw new JobNotFoundException();
			}
			_rep.Delete(job);
			await _rep.SaveChangesAsync();
		}

        public async Task<List<GetJobDto>> FindAll(Expression<Func<Job,bool>> expression, params string[] includes)
        {
			var jobs= await _rep.FindAll(expression, includes).ToListAsync();

            return _mapper.Map<List<GetJobDto>>(jobs);
        }

        public async Task<List<GetJobDto>> GetAll(params string[] includes)
		{
			var jobs = await _rep.GetAll(includes).ToListAsync();
			return _mapper.Map<List<GetJobDto>>(jobs);
		}

		public async Task<GetJobDto> GetById(int id, params string[] includes)
		{
			if (id <= 0)
			{
				throw new NegativeOrZeroIdException();
			}

			var job=await _rep.GetById(id, includes);

			if (job == null)
			{
				throw new JobNotFoundException();
			}
			GetJobDto dto = _mapper.Map<GetJobDto>(job);

			return dto;
		}

		public async Task Update(UpdateJobDto dto)
		{
			//if (dto.Id <= 0)
			//{
			//    throw new NegativeOrZeroIdException();
			//}
			var job = await _rep.GetById(dto.Id);
			if (job == null)
			{
				throw new Exception("Bu idli vakansiya yoxdur");
			}
            if (dto.Requirements == "<p><br></p>")
            {
                dto.Requirements = job.Requirements;
            }
            if (dto.Description == "<p><br></p>")
            {
                dto.Description = job.Description;
            }

            dto.CompanyId = job.CompanyId;
            _mapper.Map(dto,job);
			_rep.Update(job);
			job.UpdatedAt = DateTime.Now;
			await _rep.SaveChangesAsync();
        }
        public async Task MakeJobPremium(int id, string stripeEmail, string stripeToken)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }

            var job = await _rep.GetById(id,"Company");

            if (job == null)
            {
                throw new JobNotFoundException();
            }
			if (job.IsPremium)
			{
				throw new JobAlreadyPremiumException("Bu elan premiumdur.");
			}
            GetJobDto dto = _mapper.Map<GetJobDto>(job);

            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = job.Company.CompanyName,
                Phone = "+994 50 66"
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);

            var optionsCharge = new ChargeCreateOptions
            {

                Amount = (long)300,
                Currency = "USD",
                Description = "Product Selling amount",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status != "succeeded")
            {
                throw new Exception("Odenisde problem var");
            }
 
            job.IsPremium = true;
            var premiumEnd= DateTime.Now.AddDays(7);
            if (premiumEnd >= job.EndTime)
            {
                throw new PremiumEndGreaterThanEndTime();
            }
            else
            {
                job.PremiumEndTime = premiumEnd;
            }
            _rep.Update(job);
            await _rep.SaveChangesAsync();
        }

        public async Task CheckAndUpdatePremiumJobs()
        {
            var jobs =  _rep.FindAll(j => j.IsPremium && j.PremiumEndTime < DateTime.Now);

            foreach (var job in jobs)
            {
                job.IsPremium = false;
                job.PremiumEndTime = DateTime.MinValue;

                _rep.Update(job);
            }
            await _rep.SaveChangesAsync(); 
        }
    }
}


