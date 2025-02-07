using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.DTOs.Industry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<GetCompanyDto> GetById(int id);
        Task<List<GetCompanyDto>> GetAll();
        Task Create(CreateCompanyDto dto);
        Task Update(UpdateCompanyDto dto);
        Task Delete(int id);
    }
}
