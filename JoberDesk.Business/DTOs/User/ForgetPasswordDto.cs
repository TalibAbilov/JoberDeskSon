using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.User
{
    public record ForgetPasswordDto
    {
        public string? Email {get; set;}
    }
    public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("E-poçt adresi boş ola bilmez.")
                .NotEmpty()
                .WithMessage("E-poçt adresi doldur.")
                .EmailAddress()
                .WithMessage("Düzgün e-poçt formatı daxil edin.");
        }
    }
}
