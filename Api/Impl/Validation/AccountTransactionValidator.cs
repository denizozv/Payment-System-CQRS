using FluentValidation;
using Schema;

namespace Api.Impl.Validation;

public class AccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
{
    public AccountTransactionValidator()
    {
        RuleFor(x => x.AccountId).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(255);
        RuleFor(x => x.TransactionDate).NotEmpty();

        RuleFor(x => x.ReferenceNumber)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TransferType)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.TransferType));

        RuleFor(x => x)
            .Must(x => x.DebitAmount != null || x.CreditAmount != null)
            .WithMessage("Either DebitAmount or CreditAmount must be provided.");
    }
}
