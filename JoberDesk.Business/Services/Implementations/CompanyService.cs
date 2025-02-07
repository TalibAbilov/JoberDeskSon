using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.Helpers.Exceptions.Base;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public CompanyService(ICompanyRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task Create(CreateCompanyDto dto)
        {

            var isExsist = await _rep.IsExist(x => x.CompanyName == dto.CompanyName);
            if (isExsist)
            {
                throw new CategoryNameExsistException("Bu adda şirkət mövcuddur.");
            }
            var company = _mapper.Map<Company>(dto);
            await _rep.Create(company);
            await _rep.SaveChangesAsync();
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
                throw new CategoryNotFoundException("Bu id-li company mövcud deyil!");
            }
            _rep.Delete(company);
            await _rep.SaveChangesAsync();
        }

        public async Task<List<GetCompanyDto>> GetAll()
        {
            var companies = await _rep.GetAll().ToListAsync();
            return _mapper.Map<List<GetCompanyDto>>(companies);
        }

        public async Task<GetCompanyDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            GetCompanyDto dto = _mapper.Map<GetCompanyDto>(await _rep.GetById(id));

            return dto != null ? dto : throw new CategoryNotFoundException("Bu id-li company mövcud deyil!");
        }

        public async Task Update(UpdateCompanyDto dto)
        {
            //if (dto.Id <= 0)
            //{
            //    throw new NegativeOrZeroIdException();
            //}
            var company = await _rep.GetById(dto.Id);
            if (company == null)
            {
                throw new CategoryNotFoundException();
            }
            var isExsist = await _rep.IsExist(x => x.CompanyName == dto.CompanyName && x.Id != dto.Id);
            if (isExsist)
            {
                throw new CategoryNameExsistException("Bu adda şirkət mövcuddur.");
            }
            var newCompany = _mapper.Map<Company>(dto);
            newCompany.UpdatedAt = DateTime.Now;
            _rep.Update(newCompany);
            await _rep.SaveChangesAsync();
        }
    }
}
