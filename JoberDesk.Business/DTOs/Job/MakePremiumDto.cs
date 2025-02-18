using FluentValidation;
using JoberDesk.Business.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Job
{
    public record MakePremiumDto
    {
        public int Id { get; set; }
        public int Days { get; set; }
    }
    //public class MakePremiumDtoValidator : AbstractValidator<MakePremiumDto>
    //{
    //    public MakePremiumDtoValidator()
    //    {
    //        RuleFor(x => x.Days)
    //            .NotNull()
    //            .WithMessage("Premium müddəti daxil edin.")
    //            .NotEmpty()
    //            .WithMessage("Premium müddəti daxil edin.");
    //    }
    //}
}
