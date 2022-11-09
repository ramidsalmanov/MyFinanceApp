using FluentValidation;
using MyFinance.Core.Dto.Account;

namespace MyFinance.WebApi.FluentValidator;

public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.Type).NotNull();
        RuleFor(x => x.Currency).NotNull();
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
    }
}