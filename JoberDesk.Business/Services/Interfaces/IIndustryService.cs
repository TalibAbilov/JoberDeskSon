using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface IIndustryService
    {
        Task<GetIndustryDto> GetById(int id,params string[] includes);
        Task<List<GetIndustryDto>> GetAll(params string[] includes);
        Task Create(CreateIndustryDto dto);
        Task Update(UpdateIndustryDto dto);
        Task Delete(int id);
        Task<List<GetIndustryDto>> FindAll(Expression<Func<Industry, bool>> expression, params string[] includes);

    }
}
