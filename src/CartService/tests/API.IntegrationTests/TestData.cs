using DAL.Dto;

namespace API.IntegrationTests;
public static class TestData
{

    public static DAL.Models.Cart CartItem()
    {
        return new DAL.Models.Cart
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };
    }


    public static List<DAL.Models.Cart> CartItems()
    {
        return new List<DAL.Models.Cart>
        {
             new DAL.Models.Cart
                {
                    Id = 1,
                    Image = "image_url",
                    Name = "Test Item",
                    Price = 10.5m,
                    Quantity = 2
                },
              new DAL.Models.Cart
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

