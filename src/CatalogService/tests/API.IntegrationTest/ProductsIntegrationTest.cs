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

        var request = new AddProductRequest
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
        var result = await response.Content.ReadFromJsonAsync<Response<long>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Message.ShouldBe(ResponseMessage.ProductAdded);
    }


}
