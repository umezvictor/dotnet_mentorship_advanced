using DAL.Models;

namespace DAL.UnitTest;

public class CartTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_ForValidCartItem()
    {
        var product = new Cart
        {
            Name = "Test cart item",
            Price = 10.0M,
            Id = 5
        };

        Assert.True(product.IsValid());
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_ForInvalidPrice()
    {
        var product = new Cart
        {
            Name = "Test ProductTest",
            Price = 0M,
            Id = 5
        };

        Assert.False(product.IsValid());
    }



}