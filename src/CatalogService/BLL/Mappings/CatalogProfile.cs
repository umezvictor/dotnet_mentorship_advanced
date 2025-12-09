using AutoMapper;
using DAL.Entities;
using Shared.Dto;

namespace BLL.Mappings;
public class CatalogProfile : Profile
{
	public CatalogProfile ()
	{
		CreateMap<Category, CategoryDto>().ReverseMap();
		CreateMap<AddCategoryRequest, Category>();

		CreateMap<UpdateCategoryRequest, Category>()
		   .ForAllMembers( opts =>
			   opts.Condition( (src, dest, srcMember) => srcMember != null ) );

	}
}
