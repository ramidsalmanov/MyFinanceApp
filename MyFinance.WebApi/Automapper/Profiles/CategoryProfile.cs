using AutoMapper;
using MyFinance.Core.Dto;
using MyFinance.Domain.Models;

namespace MyFinance.WebApi.Automapper.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ReverseMap();

        CreateMap<Category, DtoWithName>();
    }
}