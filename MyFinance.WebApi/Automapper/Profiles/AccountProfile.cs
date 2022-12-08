using AutoMapper;
using MyFinance.Core.Dto.Account;
using MyFinance.Core.Dto.Common;
using MyFinance.Domain.Models;

namespace MyFinance.WebApi.Automapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(d => d.Type, m => m.MapFrom(s => s.Type))
            .ReverseMap()
            .ForMember(x => x.Currency, m => m.Ignore())
            .ForMember(x => x.CurrencyId, m => m.MapFrom(s => s.Currency.Id))
            .ForMember(d => d.Type, m => m.Ignore())
            .ForMember(d => d.TypeId, m => m.MapFrom(s => s.Type.Id));
        
        CreateMap<AccountType, EntityWithNameDto>().ReverseMap();
    }
}