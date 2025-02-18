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
        Task<GetCompanyDto> GetById(int id, params string[] includes);
        Task<List<GetCompanyDto>> GetAll(params string[] includes);
        Task Create(CreateCompanyDto dto,string appUserId,string stripeEmail,string stripeToken);
        Task Update(UpdateCompanyDto dto,string appUserId);
        Task Delete(int id);
    }
}
