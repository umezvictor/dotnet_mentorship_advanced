using Shared.Dto;
using Shared.ResponseObjects;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

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

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/categories", request);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Message.ShouldBe(ResponseMessage.CategoryAdded);
    }






    [Fact]
    public async Task Getcategory_ValidPayload_ShouldReturnCategoryAddedSuccessfully()
    {


        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"/api/categories");
        var result = await response.Content.ReadFromJsonAsync<Response<List<CategoryDto>>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.ShouldBeOfType<List<CategoryDto>>();
    }
}
