using BLL.Features.Products.Add;
using Shared;
using Shared.Dto;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace API.IntegrationTest;
public class ProductsIntegrationTest : BaseFunctionalTest
{
    public ProductsIntegrationTest(CatalogWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Addproduct_ValidPayload_ShouldReturnProductAddedSuccessfully()
    {

        var request = new AddProductCommand
        {
            Amount = 10,
            CategoryId = 1,
            Description = "Test product description",
            Name = "Test Product",
            Price = 99.99M,
            Image = "test-image-url"

        };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/products", request);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Message.ShouldBe(ResponseMessage.ProductAdded);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnProductsList()
    {

        HttpResponseMessage response = await HttpClient.GetAsync("/api/products");
        var result = await response.Content.ReadFromJsonAsync<Response<List<ProductDto>>>();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.Count.ShouldBeGreaterThan(0);
    }
}
