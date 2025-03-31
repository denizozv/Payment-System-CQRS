using FluentValidation;
using Schema;

namespace Api.Impl.Validation;

public class EftTransactionValidator : AbstractValidator<EftTransactionRequest>
{
    public EftTransactionValidator()
    {
        RuleFor(x => x.FromAccountId).GreaterThan(0);
        RuleFor(x => x.ReveiverIban).NotEmpty().Length(26);
        RuleFor(x => x.ReceiverName).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.TransactionDate).NotEmpty();
        RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(50);
    }
}
