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
        Task<GetCategoryDto> GetById(int id, params string[] includes);
        Task<List<GetCategoryDto>> GetAll(params string[] includes);
        Task Create(CreateCategoryDto dto);
        Task Update(UpdateCategoryDto dto);
        Task Delete(int id);

    }
}
