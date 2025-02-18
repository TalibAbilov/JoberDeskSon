using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.Helpers.Exceptions.Base;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Helpers.Exceptions.Company;
using JoberDesk.Business.Helpers.Extensions;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        readonly ICompanyRepository _rep;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _env;
        readonly IIndustryCompanyRepository _industryCompanyRepository;
        readonly IIndustryRepository _industryRepository;
        readonly IUserService _userService;

        public CompanyService(ICompanyRepository rep, IMapper mapper, IWebHostEnvironment env, IIndustryCompanyRepository industryCompanyRepository, IIndustryRepository industryRepository, IUserService userService)
        {
            _rep = rep;
            _mapper = mapper;
            _env = env;
            _industryCompanyRepository = industryCompanyRepository;
            _industryRepository = industryRepository;
            _userService = userService;
        }

        public async Task Create(CreateCompanyDto dto, string appUserId, string stripeEmail, string stripeToken)
        {
            var user = await _userService.GetById(appUserId);
            if (user.CompanyId is not null)
            {
                throw new CompanyAlreadyExistsException();
            }
            var isExist = await _rep.IsExist(x => x.CompanyName == dto.CompanyName);
            if (isExist)
            {
                throw new CompanyNameExistsException();
            }
            if (dto.file == null)
            {
                throw new CompanyLogoException("Fayl seç!");
            }
            if (!dto.file.ContentType.Contains("image"))
            {
                throw new CompanyLogoException("Düzgün fayl tipi seçin.");
            }

            if (dto.file.Length > 2097152)
            {
                throw new CompanyLogoException("Fayl ölçüsü ən çox 2MB ola bilər.");
            }

            dto.Logo = dto.file.Upload(_env.WebRootPath, "Upload/Company");

            var company = _mapper.Map<Company>(dto);
            company.AppUserId = appUserId;

            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = user.UserName,
                Phone = "+994 50 66"
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);

            var optionsCharge = new ChargeCreateOptions
            {

                Amount = (long)800,
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

            await _rep.Create(company);
			await _rep.SaveChangesAsync();


            user.CompanyId=company.Id;

			if (dto.IndustryIds != null && dto.IndustryIds.Any())
            {
                var industryCompanyList = new List<IndustryCompany>();

                foreach (var industryId in dto.IndustryIds)
                {

                    bool industryExists = await _industryRepository.IsExist(x => x.Id == industryId);
                    if (!industryExists)
                    {
                        throw new CompanyIndustryException("Seçilən sənaye mövcud deyil.");
                    }

                    industryCompanyList.Add(new IndustryCompany
                    {
                        CompanyId = company.Id,
                        IndustryId = industryId
                    });
                }

               
                if (industryCompanyList.Any())
                {
                    await _industryCompanyRepository.AddRange(industryCompanyList);
                    await _industryCompanyRepository.SaveChangesAsync();
                }
            }
            else
            {
                throw new CompanyIndustryException("Sənaye tipi seçilməlidir.");
            }
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            var company = await _rep.GetById(id);
            if (company == null)
            {
                throw new CompanyNotFounException();
            }
            _rep.Delete(company);
            await _rep.SaveChangesAsync();
        }

        public async Task<List<GetCompanyDto>> GetAll(params string[] includes)
        {
            var companies = await _rep.GetAll(includes).ToListAsync();
            return _mapper.Map<List<GetCompanyDto>>(companies);
        }

        public async Task<GetCompanyDto> GetById(int id, params string[] includes)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            var company = await _rep.GetById(id, includes);
            if (company == null)
            {
                throw new CompanyNotFounException();
            }
            GetCompanyDto dto = _mapper.Map<GetCompanyDto>(company);

            return dto;
        }

		public async Task Update(UpdateCompanyDto dto, string appUserId)
		{
			var user = await _userService.GetById(appUserId);
			if (user.CompanyId is null)
			{
				throw new CompanyNotFounException("Şirkət tapılmadı.");
			}

			var company = await _rep.GetById(user.CompanyId.Value);
			if (company is null)
			{
				throw new CompanyNotFounException("Şirkət mövcud deyil.");
			}

			if (dto.CompanyName != company.CompanyName)
			{
				bool isExist = await _rep.IsExist(x => x.CompanyName == dto.CompanyName);
				if (isExist)
				{
					throw new CompanyNameExistsException();
				}
			}

			if (dto.file is not null)
			{
				if (!dto.file.ContentType.Contains("image"))
				{
					throw new CompanyLogoException("Düzgün fayl tipi seçin.");
				}

				if (dto.file.Length > 2097152)
				{
					throw new CompanyLogoException("Fayl ölçüsü ən çox 2MB ola bilər.");
				}
				dto.Logo = dto.file.Upload(_env.WebRootPath, "Upload/Company");

                FileExtension.Delete(_env.WebRootPath, "Upload/Company",company.Logo);
			}
            else
            {
                dto.Logo = company.Logo;
            }
            if(dto.About == "<p><br></p>")
            {
                dto.About = company.About;
            }
            
			_mapper.Map(dto,company);
             _rep.Update(company);
            company.UpdatedAt = DateTime.Now;
			await _rep.SaveChangesAsync();

			if (dto.IndustryIds != null && dto.IndustryIds.Any())
			{
				var existingIndustries = await _industryCompanyRepository.FindAll(x => x.CompanyId == company.Id).ToListAsync();
				_industryCompanyRepository.RemoveRange(existingIndustries);

				var newIndustryCompanyList = new List<IndustryCompany>();
				foreach (var industryId in dto.IndustryIds)
				{
					bool industryExists = await _industryRepository.IsExist(x => x.Id == industryId);
					if (!industryExists)
					{
						throw new CompanyIndustryException("Seçilən sənaye mövcud deyil.");
					}

					newIndustryCompanyList.Add(new IndustryCompany
					{
						CompanyId = company.Id,
						IndustryId = industryId
					});
				}

				await _industryCompanyRepository.AddRange(newIndustryCompanyList);
				await _industryCompanyRepository.SaveChangesAsync();
			}
			else
			{
				throw new CompanyIndustryException("Sənaye tipi seçilməlidir.");
			}
		}

	}
}
