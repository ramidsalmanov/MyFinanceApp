using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MyFinance.WebApi.Controllers;

public class BaseController : ControllerBase
{
    public IMapper Mapper { get; protected set; }

    public BaseController(IMapper mapper)
    {
        Mapper = mapper;
    }
}