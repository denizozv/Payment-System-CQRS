using FluentValidation;
using Schema;

namespace Api.Impl.Validation;

public class MoneyTransferValidator : AbstractValidator<MoneyTransferRequest>
{
    public MoneyTransferValidator()
    {
        RuleFor(x => x.FromAccountId).GreaterThan(0);
        RuleFor(x => x.ToAccountId).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.TransactionDate).NotEmpty();
        RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(50);
    }
}
