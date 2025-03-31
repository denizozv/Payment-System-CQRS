using FluentValidation;
using Api.Domain;
using Schema;

namespace Api.Impl.Validation;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(50);
        When(x => !string.IsNullOrEmpty(x.MiddleName), () => RuleFor(x => x.MiddleName).MinimumLength(2).MaximumLength(50));
        RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.IdentityNumber).MinimumLength(11).MaximumLength(11);        
    }
}