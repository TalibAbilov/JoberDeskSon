using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Setting
{
    public record CreateSettingDto
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
    public class CreateSettingDtoValidator : AbstractValidator<CreateSettingDto>
    {
        public CreateSettingDtoValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty()
                .WithMessage("Açar daxil edin.")
                .NotNull()
                .WithMessage("Açar daxil edin.");
            RuleFor(x => x.Value)
               .NotEmpty()
               .WithMessage("Dəyər daxil edin.")
               .NotNull()
               .WithMessage("Dəyər daxil edin.");
        }
    }
}
