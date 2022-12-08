using AutoMapper;
using MyFinance.Core.Dto.Budget;
using MyFinance.Domain.Models.Budget;

namespace MyFinance.WebApi.Automapper.Profiles;

public class BudgetProfile : Profile
{
    public BudgetProfile()
    {
        CreateMap<Budget, BudgetDto>().ReverseMap();
        CreateMap<BudgetItem, BudgetItemDto>().ReverseMap();
    }
}