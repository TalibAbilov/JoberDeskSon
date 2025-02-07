using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,GetCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<GetCategoryDto, UpdateCategoryDto>().ReverseMap();

        }
    }
}
