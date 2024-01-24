using API.Resources;
using FluentValidation;

namespace API.Validators
{
    public class SaveCompanyResourceValidator : AbstractValidator<SaveCompanyResource>
    {
        public SaveCompanyResourceValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.CompanyId)
                .NotEmpty()
                .WithMessage("Company ID must not be 0.");

            RuleFor(m => m.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee ID must not be 0.");
        }
    }
}
