using AutoMapper;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Domain.Dto;

namespace SoftPlex.Task.Core.Infrastructure.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<ProductPostDto, Product>();
    }
}