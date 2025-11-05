using DAL.Entities;
using Mongo2Go;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace API.IntegrationTests;
public class IntegrationTests : IClassFixture<IntegrationTests.TestContext>
{
    private readonly HttpClient _client;

    public class TestContext : IDisposable
    {
        public MongoDbRunner MongoRunner { get; }
        public CartWebApplicationFactory Factory { get; }

        public TestContext()
        {
            MongoRunner = MongoDbRunner.Start();
            Factory = new CartWebApplicationFactory(MongoRunner);
        }

        public void Dispose()
        {
            Factory.Dispose();
            MongoRunner.Dispose();
        }
    }

    public IntegrationTests(TestContext ctx)
    {
        _client = ctx.Factory.CreateClient();
    }

    [Fact]
    public async Task AddItemToCart_GivenValidPayload_ShouldReturnOk()
    {
        var request = new Cart
        {
            CartKey = "1234",
            CartItems = new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    Name = "Test Product",
                    Image = "test-image-url",
                    Price = 99.99M,
                    Quantity = 2
                },
                 new CartItem
                {
                    Id = 2,
                    Name = "Test Product2",
                    Image = "test-image-url2",
                    Price = 99.99M,
                    Quantity = 2
                }
            }
        };

        var response = await _client.PostAsJsonAsync("/api/v1/cart", request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Response<string>>();
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
    }





}

