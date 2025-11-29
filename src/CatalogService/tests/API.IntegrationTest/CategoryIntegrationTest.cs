using Shared.Dto;
using Shared.ResponseObjects;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace API.IntegrationTest;
public class CategoryIntegrationTest : BaseFunctionalTest
{
    // private readonly HttpClient _client;
    public CategoryIntegrationTest(CatalogWebApplicationFactory factory) : base(factory)
    {
        // _client = factory.CreateClient();
    }

    //[Fact]
    //public async Task Addcategory_ValidPayload_ShouldReturnCategoryAddedSuccessfully()
    //{

    //    var request = new AddCategoryRequest
    //    {
    //        Name = "Test Category",
    //        Image = "test-image-url"

    //    };


    //    //var token = TokenGenerator.GenerateAccessToken();
    //    //_client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

    //    // Act
    //    HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/categories", request);
    //    var result = await response.Content.ReadFromJsonAsync<Response<string>>();

    //    // Assert
    //    response.StatusCode.ShouldBe(HttpStatusCode.OK);
    //    result.ShouldNotBeNull();
    //    result.Succeeded.ShouldBeTrue();
    //    result.Message.ShouldBe(ResponseMessage.CategoryAdded);
    //}






    //[Fact]
    //public async Task Getcategory_ValidPayload_ShouldReturnCategoryAddedSuccessfully()
    //{
    //    // Arrange
    //    //var token = TokenGenerator.GenerateAccessToken();
    //    //_client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

    //    // Act
    //    HttpResponseMessage response = await HttpClient.GetAsync($"/api/categories");
    //    var result = await response.Content.ReadFromJsonAsync<Response<List<CategoryDto>>>();

    //    // Assert
    //    response.StatusCode.ShouldBe(HttpStatusCode.OK);
    //    result.Succeeded.ShouldBeTrue();
    //    result.Data.ShouldNotBeNull();
    //    result.Data.ShouldBeOfType<List<CategoryDto>>();
    //}





    [Fact]
    public async Task Addcategory_ValidPayload_ShouldReturnCategoryAddedSuccessfully()
    {
        var request = new AddCategoryRequest
        {
            Name = "Test Category",
            Image = "test-image-url"
        };

        var response = await HttpClient.PostAsJsonAsync("/api/categories", request);

        // Assert status first; include raw body for troubleshooting when it fails
        var rawBody = await response.Content.ReadAsStringAsync();
        response.StatusCode.ShouldBe(HttpStatusCode.OK, $"Unexpected status. Body: '{rawBody}'");

        // Guard against empty body (previous JsonException source)
        rawBody.ShouldNotBeNullOrEmpty("Response body was empty – endpoint likely returned 403/400 without JSON.");

        var result = JsonSerializer.Deserialize<Response<string>>(rawBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Message.ShouldBe(ResponseMessage.CategoryAdded);
    }

    [Fact]
    public async Task Getcategory_ValidPayload_ShouldReturnCategoryList()
    {
        var response = await HttpClient.GetAsync("/api/categories");
        var rawBody = await response.Content.ReadAsStringAsync();
        response.StatusCode.ShouldBe(HttpStatusCode.OK, $"Unexpected status. Body: '{rawBody}'");

        var result = JsonSerializer.Deserialize<Response<List<CategoryDto>>>(rawBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.ShouldBeOfType<List<CategoryDto>>();
    }
}
