using AutoMapper;
using MyFinance.Core.Dto;
using MyFinance.Domain.Models;

namespace MyFinance.WebApi.Automapper.Profiles;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<Currency, CurrencyDto>().ReverseMap();
    }
}