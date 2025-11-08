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



    [Fact]
    public async Task Addproduct_InValidPayload_ShouldReturnBadRequest()
    {

        var request = new AddProductRequest
        {
            Amount = 10,
            CategoryId = 1,
            Description = "Test product description",
            Price = 99.99M,
            Image = "test-image-url"

        };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/products", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);


    }



    [Fact]
    public async Task Getproduct_ValidPayload_ShouldReturnProductAddedSuccessfully()
    {

        var query = new GetProductsQuery
        {
            CategoryId = 1,
            PageNumber = 1,
            PageSize = 10
        };

        await AddProduct();
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"/api/products?categoryId={query.CategoryId}&pageNumber={query.PageNumber}&pageSize={query.PageSize}");
        var result = await response.Content.ReadFromJsonAsync<Response<PaginatedResponse<List<ProductDto>>>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.Succeeded.ShouldBeTrue();
        result.Data.Data.ShouldNotBeNull();
        result.Data.Data.ShouldBeOfType<List<ProductDto>>();
    }



    private async Task AddProduct()
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


        await HttpClient.PostAsJsonAsync("/api/products", request);


    }
}
