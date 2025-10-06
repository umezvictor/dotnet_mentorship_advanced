using BLL.Features.Add;
using CartService.Domain;
using CartService.Shared.Dto;

namespace TestSuite;
public static class TestData
{
    public static Cart CartItem()
    {
        return new Cart
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };
    }

    public static AddToCartCommand AddToCartCommand()
    {
        return new AddToCartCommand
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };
    }

    public static List<Cart> CartItems()
    {
        return new List<Cart>
        {
             new Cart
                {
                    Id = 1,
                    Image = "image_url",
                    Name = "Test Item",
                    Price = 10.5m,
                    Quantity = 2
                },
              new Cart
                {
                    Id = 2,
                    Image = "image_url",
                    Name = "Test Item2",
                    Price = 10.5m,
                    Quantity = 2
                }
        };
    }



    public static List<CartDto> CartItemsDto()
    {
        return new List<CartDto>
        {
             new CartDto
                {
                    Id = 1,
                    Image = "image_url",
                    Name = "Test Item",
                    Price = 10.5m,
                    Quantity = 2
                },
              new CartDto
                {
                    Id = 2,
                    Image = "image_url",
                    Name = "Test Item2",
                    Price = 10.5m,
                    Quantity = 2
                }
        };
    }
}
