using AutoMapper;
using BLL.Features.Categories.Add;
using BLL.Features.Categories.Update;
using Domain.Entities;
using Shared.Dto;

namespace BLL.Mappings;
public class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<AddCategoryCommand, Category>();

        CreateMap<UpdateCategoryCommand, Category>()
           .ForAllMembers(opts =>
               opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}
