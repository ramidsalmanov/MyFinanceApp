using AutoMapper;
using MyFinance.Core.Dto;
using MyFinance.Domain.Models.Transactions;

namespace MyFinance.WebApi.Automapper.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDto>()
            .ReverseMap()
            .ForMember(d => d.Category, m => m.Ignore());

        CreateMap<Transfer, TransferDto>().ReverseMap();
    }
}