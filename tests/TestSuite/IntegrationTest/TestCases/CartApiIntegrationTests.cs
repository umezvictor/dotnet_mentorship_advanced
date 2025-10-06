using BLL.Features.Add;
using CartService.Shared.Dto;
using Mongo2Go;
using Shared;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace TestSuite.IntegrationTest.TestCases;
public class CartApiIntegrationTests : IClassFixture<CartApiIntegrationTests.BooksTestContext>
{
    private readonly HttpClient _client;

    public class BooksTestContext : IDisposable
    {
        public MongoDbRunner MongoRunner { get; }
        public CustomWebApplicationFactory Factory { get; }

        public BooksTestContext()
        {
            MongoRunner = MongoDbRunner.Start();
            Factory = new CustomWebApplicationFactory(MongoRunner);
        }

        public void Dispose()
        {
            Factory.Dispose();
            MongoRunner.Dispose();
        }
    }

    public CartApiIntegrationTests(BooksTestContext ctx)
    {
        _client = ctx.Factory.CreateClient();
    }

    [Fact]
    public async Task AddItemToCart_GivenValidPayload_ShouldReturnOk()
    {
        var command = new AddToCartCommand
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };

        var response = await _client.PostAsJsonAsync("/api/cart", command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
    }


    [Fact]
    public async Task DeleteItemFromCart_GivenValidId_ShouldRemoveItemAndReturnOk()
    {

        var response = await _client.DeleteAsync($"/api/cart/{1}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();

    }



    [Fact]
    public async Task GetCartItems_GivenDataExists_ShouldReturnCartItemsAndReturnOk()
    {


        var response = await _client.GetAsync($"/api/cart");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<List<CartDto>>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldBeOfType<List<CartDto>>();



    }

}
