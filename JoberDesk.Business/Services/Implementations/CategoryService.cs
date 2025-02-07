using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.Helpers.Exceptions.Base;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _rep;
        readonly IMapper _mapper;

        public CategoryService(ICategoryRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task Create(CreateCategoryDto dto)
        {
            var isExsist=await _rep.IsExist(x=>x.Name==dto.Name);
            if (isExsist)
            {
                throw new CategoryNameExsistException();
            }
            var category = _mapper.Map<Category>(dto);
            await _rep.Create(category);
            await _rep.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if(id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            var category=await _rep.GetById(id);
            if(category == null)
            {
                throw new CategoryNotFoundException();
            }
            _rep.Delete(category);
            await _rep.SaveChangesAsync();
        }
        public async Task<List<GetCategoryDto>> GetAll()
        {
            var categories= await _rep.GetAll().ToListAsync();
            return  _mapper.Map<List<GetCategoryDto>>(categories);
        }

        public async Task<GetCategoryDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroIdException();
            }
            GetCategoryDto dto = _mapper.Map<GetCategoryDto>(await _rep.GetById(id));

            return dto != null ? dto :throw new CategoryNotFoundException();
        }

        public async Task Update(UpdateCategoryDto dto)
        {
            //if (dto.Id <= 0)
            //{
            //    throw new NegativeOrZeroIdException();
            //}
            var category=await _rep.GetById(dto.Id);
            if(category == null)
            {
                throw new CategoryNotFoundException();
            }
            var isExsist = await _rep.IsExist(x => x.Name == dto.Name && x.Id!=dto.Id);
            if (isExsist)
            {
                throw new CategoryNameExsistException();
            }
            var newCategory = _mapper.Map<Category>(dto);
            newCategory.UpdatedAt=DateTime.Now;
            _rep.Update(newCategory);
            await _rep.SaveChangesAsync();
     
        }

    }
}
