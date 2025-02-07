using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Mappers
{
    public class IndustryMapper : Profile
    {
        public IndustryMapper()
        {
            CreateMap<Industry, CreateIndustryDto>().ReverseMap();
            CreateMap<Industry, GetIndustryDto>().ReverseMap();
            CreateMap<Industry, UpdateIndustryDto>().ReverseMap();
            CreateMap<GetIndustryDto, UpdateIndustryDto>().ReverseMap();
        }
    }
}
