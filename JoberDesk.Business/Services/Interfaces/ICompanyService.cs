using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<List<GetCompanyDto>> FindAll(Expression<Func<Company, bool>> expression, params string[] includes);

    }
}
