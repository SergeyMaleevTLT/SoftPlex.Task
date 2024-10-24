using AutoMapper;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Web.Models;

namespace SoftPlex.Task.Web.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductPostViewModel, ProductPostDto>().ReverseMap();
        CreateMap<ProductViewModel, ProductDto>().ReverseMap();
    }
}