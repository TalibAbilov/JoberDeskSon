using JoberDesk.Business.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<GetCategoryDto> GetById(int id);
        Task<List<GetCategoryDto>> GetAll();
        Task Create(CreateCategoryDto dto);
        Task Update(UpdateCategoryDto dto);
        Task Delete(int id);

    }
}
