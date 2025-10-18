using Shared.Dto;

namespace Cart.IntegrationTest;
public static class TestData
{
    public static CartService.Domain.Cart CartItem()
    {
        return new CartService.Domain.Cart
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };
    }


    public static List<CartService.Domain.Cart> CartItems()
    {
        return new List<CartService.Domain.Cart>
        {
             new CartService.Domain.Cart
                {
                    Id = 1,
                    Image = "image_url",
                    Name = "Test Item",
                    Price = 10.5m,
                    Quantity = 2
                },
              new CartService.Domain.Cart
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

