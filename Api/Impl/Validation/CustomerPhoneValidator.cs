using FluentValidation;
using Schema;

namespace Api.Impl.Validation;

public class CustomerPhoneValidator : AbstractValidator<CustomerPhoneRequest>
{
    public CustomerPhoneValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.CountryCode).NotEmpty().Length(2, 3);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(12);
    }
}
