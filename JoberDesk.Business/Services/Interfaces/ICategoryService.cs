using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<GetCategoryDto> GetById(int id, params string[] includes);
        Task<List<GetCategoryDto>> GetAll(params string[] includes);
        Task Create(CreateCategoryDto dto);
        Task Update(UpdateCategoryDto dto);
        Task Delete(int id);
        Task<List<GetCategoryDto>> FindAll(Expression<Func<Category, bool>> expression, params string[] includes);


    }
}
