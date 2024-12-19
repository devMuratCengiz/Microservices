using FluentValidation;
using Microservices.Web.Models.Discount;

namespace Microservices.Web.Validators
{
    public class ApplyDiscountInputValidator:AbstractValidator<ApplyDiscountInput>
    {
        public ApplyDiscountInputValidator()
        {
            RuleFor(x=>x.Code).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
        }
    }
}
