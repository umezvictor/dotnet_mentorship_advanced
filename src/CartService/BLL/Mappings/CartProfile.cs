using AutoMapper;
using BLL.Features.Add;
using CartService.Domain;
using CartService.Shared.Dto;

namespace CartService.BLL.Mappings;
public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<AddToCartCommand, Cart>();

    }
}
