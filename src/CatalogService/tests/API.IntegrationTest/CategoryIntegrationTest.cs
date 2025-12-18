using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Shared.Dto;
using Shared.ResponseObjects;
using Shouldly;

namespace API.IntegrationTest;
public class CategoryIntegrationTest : BaseFunctionalTest
{
    public CategoryIntegrationTest(CatalogWebApplicationFactory factory) : base(factory)
    {
    }


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
