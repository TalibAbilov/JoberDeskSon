using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Company
{
	public record CreateCompanyDto
	{
        public string? CompanyName { get; set; }
		public string? Address { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }
        public string? Logo { get; set; } 
		public IFormFile? file { get; set; }
        public List<int>?IndustryIds { get; set; }
		
	}
    public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Şirkət adı boş ola bilməz.")
                .NotNull()
                .WithMessage("Şirkət adı daxil edin.")
                .MinimumLength(3)
                .WithMessage("Şirkət adı ən azı 3 simvoldan ibarət olmalıdır.")
                .MaximumLength(250)
                .WithMessage("Şirkət adı ən çox 250 simvoldan ibarət ola bilər.");
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
                .Must(value => !string.IsNullOrWhiteSpace(value) && value.Trim() != "<p><br></p>")
                .WithMessage("Şirkət haqqında məlumat daxil edin.")
                .MinimumLength(10)
                .WithMessage("Şirkət haqqında məlumat ən azı 3 simvoldan ibarət olmalıdır.")
                .MaximumLength(1000)
                .WithMessage("Şirkət haqqında məlumat ən çox 1000 simvoldan ibarət ola bilər.");
            RuleFor(x => x.file)
                .NotEmpty()
                .WithMessage("Logo boş ola bilməz.")
                .NotNull()
                .WithMessage("Logo seçin.");
            RuleFor(x => x.IndustryIds)
                .NotEmpty()
                .WithMessage("Sənaye tipləri seç")
                .NotNull()
                .WithMessage("Sənaye tipləri seç");
        }
    }
}
