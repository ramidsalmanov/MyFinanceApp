using MyFinance.Core.Dto;
using MyFinance.Domain.Models;

namespace MyFinance.WebApi.Helpers;

public static class Helper
{
    public static UserCategoriesDto CreateTree(List<CategoryDto> categoryDtos)
    {
        var result = new UserCategoriesDto()
        {
            Incomes = new CategoryDto()
            {
                Name = "Доходы",
                Type = CategoryType.Incomes,
                Path = "1"
            },
            Expenses = new CategoryDto()
            {
                Type = CategoryType.Expenses,
                Name = "Расходы",
                Path = "0"
            }
        };

        Create(categoryDtos.Where(x => x.Type == CategoryType.Incomes).ToList(), result.Incomes);
        Create(categoryDtos.Where(x => x.Type == CategoryType.Expenses).ToList(), result.Expenses);

        return result;
    }

    private static void Create(IList<CategoryDto> incomes, CategoryDto parent)
    {
        var children = incomes
            .Where(x => x.Path.Equals($"{parent.Path}.{x.Id}"))
            .ToList();

        if (children.Count > 0)
        {
            foreach (var child in children)
            {
                Create(incomes, child);
            }
        }

        parent.Children = children;
    }
}