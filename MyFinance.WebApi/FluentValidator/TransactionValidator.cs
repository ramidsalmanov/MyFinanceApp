using FluentValidation;
using MyFinance.Core.Dto;

namespace MyFinance.WebApi.FluentValidator;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Sum).GreaterThanOrEqualTo(0);
    }
}