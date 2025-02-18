using FluentValidation;
using JoberDesk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Job
{
	public record UpdateJobDto
	{
		public int Id { get; set; }
		//public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Requirements { get; set; }
		public int? CategoryId { get; set; }
		public int? CompanyId { get; set; }
		public DateTime? EndTime { get; set; }
		public EmploymentType? EmploymentType { get; set; }
		public ExperienceLevel? ExperienceLevel { get; set; }
		public EducationLevel? EducationLevel { get; set; }
    }
    public class UpdateJobDtoValidator : AbstractValidator<UpdateJobDto>
    {
        public UpdateJobDtoValidator()
        {
        //    RuleFor(x => x.Name)
        //        .MinimumLength(5)
        //        .WithMessage("Vakansiya adı ən azı 5 simvoldan ibarət olmalıdır.")
        //        .MaximumLength(250)
        //        .WithMessage("Vakansiya adı ən çox 250 simvoldan ibarət ola bilər.");
            RuleFor(x => x.CategoryId)
                .NotNull()
                .WithMessage("Vakansiya kateqoriyasını seçin.")
                .NotEmpty()
                .WithMessage("Vakansiya kateqoriyasını seçin.");
            RuleFor(x => x.Description)
                .Must(value => !string.IsNullOrWhiteSpace(value) && value.Trim() != "<p><br></p>")
                .WithMessage("Vakansiya haqqında məlumat daxil edin.")
                .MinimumLength(10)
                .WithMessage("Vakansiya haqqında məlumat ən azı 10 simvoldan ibarət olmalıdır.")
                .MaximumLength(1000)
                .WithMessage("Vakansiya haqqında məlumat ən çox 1000 simvoldan ibarət ola bilər.");
            RuleFor(x => x.Requirements)
                .Must(value => !string.IsNullOrWhiteSpace(value) && value.Trim() != "<p><br></p>")
                .WithMessage("Vakansiya tələblərini daxil edin.")
                .MinimumLength(10)
                .WithMessage("Vakansiya tələbləri ən azı 10 simvoldan ibarət olmalıdır.")
                .MaximumLength(1000)
                .WithMessage("Vakansiya haqqında məlumat ən çox 1000 simvoldan ibarət ola bilər.");
            RuleFor(x => x.EndTime)
                .NotNull()
                .WithMessage("Vakansiyanın bitmə tarixini seç.")
                .NotEmpty()
                .WithMessage("Vakansiyanın bitmə tarixini seç.")
                .GreaterThan(DateTime.Now)
                .WithMessage("Vakansiyanın bitmə tarixi gələcək bir tarix olmalıdır.");
            RuleFor(x => x.EmploymentType)
                .NotNull()
                .WithMessage("İş növünü seç.")
                .NotEmpty()
                .WithMessage("İş növünü seç.");
            RuleFor(x => x.EducationLevel)
                .NotNull()
                .WithMessage("Minumum təhsil tələbini seç.")
                .NotEmpty()
                .WithMessage("Minumum təhsil tələbini seç.");
            RuleFor(x => x.ExperienceLevel)
                .NotNull()
                .WithMessage("Minumum təcrübə tələbini seç.")
                .NotEmpty()
                .WithMessage("Minumum təcrübə tələbini seç.");
        }
    }
}
