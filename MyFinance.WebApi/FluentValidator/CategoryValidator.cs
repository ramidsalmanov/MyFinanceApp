using FluentValidation;
using MyFinance.Core.Dto;

namespace MyFinance.WebApi.FluentValidator;

public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        //RuleFor(x => x.ParentPath).NotEmpty().NotNull().WithMessage("ParentPath cannot be null or empty");
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}