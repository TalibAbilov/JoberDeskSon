using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface IIndustryService
    {
        Task<GetIndustryDto> GetById(int id);
        Task<List<GetIndustryDto>> GetAll();
        Task Create(CreateIndustryDto dto);
        Task Update(UpdateIndustryDto dto);
        Task Delete(int id);
    }
}
