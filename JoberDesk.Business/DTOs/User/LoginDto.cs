using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.User
{
	public class LoginDto
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
		public bool RememberMe { get; set; }
	}
	public class LoginDtoValidator : AbstractValidator<LoginDto>
	{
		public LoginDtoValidator()
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
		}
	}
}
