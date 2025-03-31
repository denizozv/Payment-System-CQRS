using FluentValidation;
using Schema;

namespace Api.Impl.Validation;

public class AccountValidator : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.AccountNumber).GreaterThan(0);
        RuleFor(x => x.IBAN).Length(26);
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CurrencyCode).Length(3);
        RuleFor(x => x.OpenDate).NotEmpty();
    }
}
