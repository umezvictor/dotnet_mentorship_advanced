using AutoMapper;
using DAL.Entities;
using Shared.Dto;

namespace BLL.Mappings;
public class ProductProfile : Profile
{
	public ProductProfile ()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<AddProductRequest, Product>();

		CreateMap<UpdateProductRequest, Product>()
		   .ForAllMembers( opts =>
			   opts.Condition( (src, dest, srcMember) => srcMember != null ) );

	}
}
