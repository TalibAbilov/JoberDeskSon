using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.User
{
    public record ResetPasswordDto
    {
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string userId { get; set; }
        public string token { get; set; }
    }
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotNull()
                .WithMessage("Şifrə boş ola bilməz.")
                .NotEmpty()
                .WithMessage("Şifrə daxil edin.")
                .MinimumLength(8)
                .WithMessage("Şifrə ən azı 8 simvoldan ibarət olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .WithMessage("Təsdiq şifrəsi boş ola bilməz.")
                .NotEmpty()
                .WithMessage("Təsdiq şifrəsi daxil edin.")
                .Matches(x => x.NewPassword)
                .WithMessage("Şifrələr uyğunlaşmır.");
        }
    }
}
