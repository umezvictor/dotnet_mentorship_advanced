using System.Net;
using System.Net.Http.Json;
using DAL;
using DAL.Entities;
using Shared.ResponseObjects;
using Shouldly;

namespace API.IntegrationTests;
public class IntegrationTests : IClassFixture<CartWebApplicationFactory>
{
    private readonly CartWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public IntegrationTests(CartWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task AddItemToCart_GivenValidPayload_ShouldReturnOk()
    {

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
        var token = TokenGenerator.GenerateAccessToken();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _client.PostAsJsonAsync("/api/v1/cart", request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
    }



    [Fact]
    public async Task GetItemFromCart_GivenValidCartKey_Version1ShouldReturnCartModel()
    {

        string cartKey = "1234";
        var token = TokenGenerator.GenerateAccessToken();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        await AddTestCartDataAsync(_client, cartKey);

        var response = await _client.GetAsync($"/api/v1/cart/{cartKey}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<Cart>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldBeOfType<Cart>();
    }



    [Fact]
    public async Task GetItemFromCart_GivenValidCartKey_Version2ShouldReturnCartModel()
    {

        string cartKey = "1234";
        var token = TokenGenerator.GenerateAccessToken();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        await AddTestCartDataAsync(_client, cartKey);

        var response = await _client.GetAsync($"/api/v2/cart/{cartKey}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<List<CartItem>>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Data.ShouldBeOfType<List<CartItem>>();
    }


    private static async Task AddTestCartDataAsync(HttpClient client, string cartKey)
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

