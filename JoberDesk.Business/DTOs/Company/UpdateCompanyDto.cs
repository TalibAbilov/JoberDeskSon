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
		public int Id { get; set; }
		public string? CompanyName { get; set; }
		public string Address { get; set; }
		public string Website { get; set; }
		public string? About { get; set; }
		public IFormFile? Logo { get; set; }
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
                .WithMessage("Şirkət adı ən çox 100 simvoldan ibarət ola bilər.");
            RuleFor(x => x.Address)
				.MinimumLength(3)
				.WithMessage("Ünvan ən azı 3 simvoldan ibarət olmalıdır.")
				.MaximumLength(250)
				.WithMessage("Ünvan ən çox 250 simvoldan ibarət ola bilər.");
            RuleFor(x => x.Website)
                .MinimumLength(3)
                .WithMessage("Keçid ən azı 3 simvoldan ibarət olmalıdır.")
                .MaximumLength(250)
                .WithMessage("Keçid ən çox 250 simvoldan ibarət ola bilər.");
            RuleFor(x => x.About)
				.NotEmpty()
				.WithMessage("Şirkət haqqında məlumat boş ola bilməz.")
				.NotNull()
				.WithMessage("Şirkət haqqında məlumat daxil edin.")
				.MinimumLength(3)
				.WithMessage("Şirkət haqqında məlumat 3 simvoldan ibarət olmalıdır.")
				.MaximumLength(250)
				.WithMessage("Şirkət haqqında məlumat ən çox 1000 simvoldan ibarət ola bilər.");
			RuleFor(x => x.Logo)
				.NotEmpty()
				.WithMessage("Logo boş ola bilməz.")
				.NotNull()
				.WithMessage("Logo seçin.");
				
		}
	}
}
