using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Business.Helpers.Exceptions.Base;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Helpers.Exceptions.Industry;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class IndustryService : IIndustryService
    {
        readonly IIndustryRepository _rep;
        readonly IMapper _mapper;

        public IndustryService(IIndustryRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task Create(CreateIndustryDto dto)
        {
            var isExsist = await _rep.IsExist(x => x.Name == dto.Name);
            if (isExsist)
            {
                throw new IndustryNameExsistException();
            }
            var industry = _mapper.Map<Industry>(dto);
            await _rep.Create(industry);
            await _rep.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            var industry = await _rep.GetById(id);
            if (industry == null)
            {
                throw new IndustryNotFoundException();
            }
            _rep.Delete(industry);
            await _rep.SaveChangesAsync();
        }
        public async Task<List<GetIndustryDto>> FindAll(Expression<Func<Industry, bool>> expression, params string[] includes)
        {
            var jobs = await _rep.FindAll(expression, includes).ToListAsync();

            return _mapper.Map<List<GetIndustryDto>>(jobs);
        }
        public async Task<List<GetIndustryDto>> GetAll(params string[] includes)
        {
            var industries = await _rep.GetAll(includes).ToListAsync();
            return _mapper.Map<List<GetIndustryDto>>(industries);
        }

        public async Task<GetIndustryDto> GetById(int id, params string[] includes)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            var industry=  await _rep.GetById(id,includes);

            GetIndustryDto dto = _mapper.Map<GetIndustryDto>(industry);

            return dto != null ? dto : throw new IndustryNotFoundException();
        }

        public async Task Update(UpdateIndustryDto dto)
        {
            //if (dto.Id <= 0)
            //{
            //    throw new NegativeOrZeroIdException();
            //}
            var industry = await _rep.GetById(dto.Id);
            if (industry == null)
            {
                throw new IndustryNotFoundException();
            }
            var isExsist = await _rep.IsExist(x => x.Name == dto.Name && x.Id != dto.Id);
            if (isExsist)
            {
                throw new IndustryNameExsistException();
            }
            var newIndustry = _mapper.Map<Industry>(dto);
            newIndustry.UpdatedAt = DateTime.Now;
            _rep.Update(newIndustry);
            await _rep.SaveChangesAsync();
        }

    }
}

