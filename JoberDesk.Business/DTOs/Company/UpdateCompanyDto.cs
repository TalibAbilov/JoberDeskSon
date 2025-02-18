using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Company
{
	public record UpdateCompanyDto
	{
		public string? CompanyName { get; set; }
		public string? Address { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }
		public string? Logo { get; set; }
		
		public IFormFile? file { get; set; }
		public List<int>? IndustryIds { get; set; }
	}
	public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
	{
		public UpdateCompanyDtoValidator()
		{
			RuleFor(x=>x.CompanyName)
                .NotEmpty()
                .WithMessage("Şirkət adı boş ola bilməz.")
                .NotNull()
                .WithMessage("Şirkət adı daxil edin.")
                .MinimumLength(3)
                .WithMessage("Şirkət adı ən azı 3 simvoldan ibarət olmalıdır.")
                .MaximumLength(250)
                .WithMessage("Şirkət adı ən çox 250 simvoldan ibarət ola bilər.");
			RuleFor(x => x.About)
				.MinimumLength(10).WithMessage("Min uzunluq 3");
			RuleFor(x => x.IndustryIds)
					.NotEmpty()
					.WithMessage("Sənaye tipləri seç")
					.NotNull()
					.WithMessage("Sənaye tipləri seç");				
		}
	}
}
