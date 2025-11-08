using DAL;
using DAL.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace API.IntegrationTests;
public class IntegrationTests : IClassFixture<CartWebApplicationFactory>
{


    [Fact]
    public async Task AddItemToCart_GivenValidPayload_ShouldReturnOk()
    {

        using var factory = new CartWebApplicationFactory();
        var client = factory.CreateClient();

        var request = new AddItemToCartRequest
        {
            CartKey = "1234",
            CartItem = new CartItem
            {
                Id = 1,
                Name = "Test Product",
                Image = "test-image-url",
                Price = 99.99M,
                Quantity = 2
            }
        };

        var response = await client.PostAsJsonAsync("/api/v1/cart", request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
    }



    [Fact]
    public async Task GetItemFromCart_GivenValidCartKey_Version1ShouldReturnCartModel()
    {
        using var factory = new CartWebApplicationFactory();
        var client = factory.CreateClient();

        string cartKey = "1234";
        await AddTestCartDataAsync(client, cartKey);

        var response = await client.GetAsync($"/api/v1/cart/{cartKey}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<Cart>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldBeOfType<Cart>();
    }



    [Fact]
    public async Task GetItemFromCart_GivenValidCartKey_Version2ShouldReturnCartModel()
    {
        using var factory = new CartWebApplicationFactory();
        var client = factory.CreateClient();

        string cartKey = "1234";
        await AddTestCartDataAsync(client, cartKey);

        var response = await client.GetAsync($"/api/v2/cart/{cartKey}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<List<CartItem>>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldBeOfType<List<CartItem>>();
    }


    private async Task AddTestCartDataAsync(HttpClient client, string cartKey)
    {

        var request = new AddItemToCartRequest
        {
            CartKey = cartKey,
            CartItem = new CartItem
            {
                Id = 1,
                Name = "Test Product",
                Image = "test-image-url",
                Price = 99.99M,
                Quantity = 2
            }
        };

        await client.PostAsJsonAsync("/api/v1/cart", request);

    }
}

