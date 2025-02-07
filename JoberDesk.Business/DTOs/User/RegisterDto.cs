using FluentValidation;
using JoberDesk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.User
{
    public class RegisterDto
    {
        public UserType? UserType { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
			RuleFor(x => x.Email)
				.NotNull()
				.WithMessage("E-poçt adresi boş ola bilmez.")
				.NotEmpty()
				.WithMessage("E-poçt adresi doldur.")
				.EmailAddress()
				.WithMessage("Düzgün e-poçt formatı daxil edin.");

			RuleFor(x => x.Password)
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
                .WithMessage("Təsdiq şifrəsi doldur.")
                .Matches(x => x.Password)
                .WithMessage("Şifrələr uyğunlaşmır.");

            RuleFor(x => x.UserType)
                .NotNull()
                .WithMessage("İstifadəçi tipi seç.")
                .NotEmpty()
                .WithMessage("İstifadəçi tipi seç.");


        }
    }
}
