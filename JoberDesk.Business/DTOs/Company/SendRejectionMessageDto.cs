using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Company
{
    public record SendRejectionMessageDto
    {
        public int CompanyId { get; set; }
        public string LastRejectionReason { get; set; }
    }
    public class SendRejectionMessageDtoValidator : AbstractValidator<SendRejectionMessageDto>
    {
        public SendRejectionMessageDtoValidator()
        {
            RuleFor(x=>x.LastRejectionReason)
                .NotEmpty()
                .WithMessage("Səbəb boş ola bilməz.")
                .NotNull()
                .WithMessage("Səbəb daxil edin.");
        }
    }
}
