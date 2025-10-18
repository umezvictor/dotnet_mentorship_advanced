using AutoMapper;
using BLL.Features.Products.Add;
using BLL.Features.Products.Update;
using Domain.Entities;
using Shared.Dto;

namespace BLL.Mappings;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<AddProductCommand, Product>();

        CreateMap<UpdateProductCommand, Product>()
           .ForAllMembers(opts =>
               opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}
