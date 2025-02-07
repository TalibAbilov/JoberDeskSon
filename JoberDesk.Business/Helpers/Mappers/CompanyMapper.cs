using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Mappers
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<Company, CreateCompanyDto>().ReverseMap();
            CreateMap<Company, GetCompanyDto>().ReverseMap();
            CreateMap<Company, UpdateCompanyDto>().ReverseMap();
            CreateMap<GetCompanyDto, UpdateCompanyDto>().ReverseMap();
        }
    }
}
