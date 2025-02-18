using JoberDesk.Business.DTOs.Industry;
using JoberDesk.Business.DTOs.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface ILayoutService
    {
        Dictionary<string, string> GetSetting();
        Task Create(CreateSettingDto dto);
        Task Update(UpdateSettingDto dto);
		Task Delete(string key);
		Task<GetSettingDto> GetByKey(string key);
    }
}
