using AutoMapper;
using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.DTOs.Setting;
using JoberDesk.Business.Helpers.Exceptions.Base;
using JoberDesk.Business.Helpers.Exceptions.Category;
using JoberDesk.Business.Helpers.Exceptions.Setting;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class LayoutService : ILayoutService
    {
        readonly ILayoutRepository _layoutRepository;
        readonly IMapper _mapper;

        public LayoutService(ILayoutRepository layoutRepository, IMapper mapper)
        {
            _layoutRepository = layoutRepository;
            _mapper = mapper;
        }

        public async  Task Create(CreateSettingDto dto)
        {
            var isExsist = await _layoutRepository.IsExist(x => x.Key == dto.Key);
            if (isExsist)
            {
                throw new SettingExsistException();
            }
            var setting = _mapper.Map<Setting>(dto);
            await _layoutRepository.Create(setting);
            await _layoutRepository.SaveChangesAsync();
        }

		public async Task Delete(string key)
		{
			var setting=await _layoutRepository.GetByKey(key);
			if (setting == null)
			{
				throw new NotFoundException("Setting tapilmadi!");
			}
			 _layoutRepository.Delete(setting);
			await _layoutRepository.SaveChangesAsync();
		}

		public async Task<GetSettingDto> GetByKey(string key)
		{
			var setting = await _layoutRepository.GetByKey(key);
			if (setting == null)
			{
				throw new NotFoundException("Setting tapilmadi!");
			}

			return _mapper.Map<GetSettingDto>(setting);
		}

		public Dictionary<string, string> GetSetting()
        {
            var setting = _layoutRepository.GetAll().ToDictionary(x => x.Key, x => x.Value);
            return setting;
        }

		public async Task Update(UpdateSettingDto dto)
		{
			if (string.IsNullOrEmpty(dto.Key))
			{
				throw new ArgumentException("Açar boş ola bilməz");
			}

			var setting = await _layoutRepository.GetByKey(dto.Key);
			if (setting == null)
			{
				throw new NotFoundException(); 
			}

			_mapper.Map(dto, setting); 

			setting.UpdatedAt = DateTime.Now; 

			_layoutRepository.Update(setting);
			await _layoutRepository.SaveChangesAsync();
		}
	}
}
