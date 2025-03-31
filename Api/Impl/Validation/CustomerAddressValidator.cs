using FluentValidation;
using Schema;

namespace Api.Impl.Validation;

public class CustomerAddressValidator : AbstractValidator<CustomerAddressRequest>
{
    public CustomerAddressValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        
        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .Length(2, 3);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.District)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .MaximumLength(10);
    }
}
