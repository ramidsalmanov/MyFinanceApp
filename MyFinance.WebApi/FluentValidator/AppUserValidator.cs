using FluentValidation;
using MyFinance.Domain.Models.Identity;

namespace MyFinance.WebApi.FluentValidator;

public class AppUserValidator : AbstractValidator<AppUser>
{
    public AppUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("{PropertyName} cannot be null");
    }
}