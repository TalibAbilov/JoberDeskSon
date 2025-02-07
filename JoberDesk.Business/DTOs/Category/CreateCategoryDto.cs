using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Category
{
    public record CreateCategoryDto
    {
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Kateqoriya adı boş ola bilməz.")
                .NotNull()
                .WithMessage("Kateqoriya adını daxil edin.")
                .MinimumLength(3)
                .WithMessage("Kateqoriya adı minumum 3 simvoldan ibarət olmalıdır.");
            RuleFor(x => x.Icon)
                .NotEmpty()
                .WithMessage("Simvol boş ola bilməz.")
                .NotNull()
                .WithMessage("Simvol daxil edin.");
        }
    }
}
