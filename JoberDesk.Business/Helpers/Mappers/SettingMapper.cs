using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.DTOs.Setting;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Mappers
{
    public class SettingMapper : Profile
    {
        public SettingMapper()
        {
            CreateMap<Setting, CreateSettingDto>().ReverseMap();
           
            CreateMap<Setting, UpdateSettingDto>().ReverseMap();
			CreateMap<Setting, GetSettingDto>().ReverseMap();
			CreateMap<UpdateSettingDto, GetSettingDto>().ReverseMap();


		}
	}
}
