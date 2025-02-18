using AutoMapper;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Mappers
{
	public class JobMapper:Profile
	{
        public JobMapper()
        {
			CreateMap<Job, CreateJobDto>().ReverseMap();
			CreateMap<Job, GetJobDto>().ReverseMap();
			CreateMap<Job, UpdateJobDto>().ReverseMap();
			CreateMap<GetJobDto, UpdateJobDto>().ReverseMap();
		}
    }
}
