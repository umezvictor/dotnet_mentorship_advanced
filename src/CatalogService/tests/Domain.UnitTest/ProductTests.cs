using Domain.Entities;

namespace Domain.UnitTest;

public class ProductTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_ForValidProduct()
    {
        var product = new Product
        {
            Name = "Test ProductTest",
            Price = 10.0M,
            Amount = 5,
            CategoryId = 1
        };

        Assert.True(product.IsValid());
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_ForInvalidPriceAndAmount()
    {
        var product = new Product
        {
            Name = "Test ProductTest",
            Price = 0M,
            Amount = 0,
            CategoryId = 1
        };

        Assert.False(product.IsValid());
    }


    [Fact]
    public void IsValid_ShouldReturnFalse_ForInvalidProductName()
    {
        var product = new Product
        {
            Name = "Test ProductTestProductTest ProductTest ProductTest ProductTest ProductTest ProductTest ProductTest ProductTest Product",
            Price = 12.0M,
            Amount = 4,
            CategoryId = 1
        };

        Assert.False(product.IsValid());
    }
}