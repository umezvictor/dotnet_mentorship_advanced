using AutoMapper;
using BLL.Features.Add;
using DAL.Dto;
using DAL.Models;

namespace CartService.BLL.Mappings;
public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<AddToCartCommand, Cart>();

    }
}
