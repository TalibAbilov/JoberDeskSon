using FluentValidation;
using JoberDesk.Business.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Industry
{
	public record CreateIndustryDto
	{
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
    public class CreateIndustryDtoValidator : AbstractValidator<CreateIndustryDto>
    {
        public CreateIndustryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Sənaye adı boş ola bilməz.")
                .NotNull()
                .WithMessage("Sənaye adını daxil edin.")
                .MinimumLength(3)
                .WithMessage("Sənaye adı minumum 3 simvoldan ibarət olmalıdır.");
            RuleFor(x => x.Icon)
                .NotEmpty()
                .WithMessage("Simvol boş ola bilməz.")
                .NotNull()
                .WithMessage("Simvol daxil edin.");
        }
    }
}
