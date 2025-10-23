using AutoMapper;
using BLL.Features.GetAll;
using CartService.DAL.Database.Repository;
using DAL.Dto;
using Moq;
using Shouldly;

namespace BLL.UnitTests;

public class GetCartItemsQueryHandlerTest
{
    private readonly Mock<ICartRepository> _cartRepoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly GetCartItemsQueryHandler _serviceToTest;

    public GetCartItemsQueryHandlerTest()
    {

        _serviceToTest = new GetCartItemsQueryHandler(_cartRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetCartItems_WhenDataExists_ShouldReturnListOfCartItems()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var cartItems = new List<DAL.Models.Cart>
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

        var cartItemsDto = new List<CartDto>
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


        _cartRepoMock.Setup(x => x.GetItemsAsync(cancellationToken)).ReturnsAsync(cartItems);
        _mapperMock.Setup(x => x.Map<List<CartDto>>(cartItems)).Returns(cartItemsDto);

        // Act
        var response = await _serviceToTest.Handle(new GetCartItemsQuery(), cancellationToken);

        // Assert

        response.ShouldBeOfType<List<CartDto>>();

    }

}

