using FluentValidation;
using JoberDesk.Business.DTOs.Job;
using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Category
{
    public record GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public ICollection<GetJobDto>Jobs { get; set; }
    }
}
