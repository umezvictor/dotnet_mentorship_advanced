using CartService;
using CartService.DAL.Database.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using Moq;

namespace API.IntegrationTests;
public class CartWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbRunner _mongoRunner;

    public CartWebApplicationFactory(MongoDbRunner mongoRunner)
    {
        _mongoRunner = mongoRunner;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                ["MONGODB_CONNECTION_STRING"] = _mongoRunner.ConnectionString,
                ["MONGODB_DATABASE"] = "CartDB"
            };
            config.AddInMemoryCollection(inMemorySettings!);
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(ICartRepository)
            );
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            //repository mock
            var mockRepo = new Mock<ICartRepository>();
            mockRepo.Setup(x => x.GetItemsAsync(CancellationToken.None)).ReturnsAsync(TestData.CartItems());
            mockRepo.Setup(x => x.AddItemAsync(TestData.CartItem(), CancellationToken.None));
            mockRepo.Setup(x => x.RemoveItemAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(true);
            services.AddSingleton(mockRepo.Object);
        });
    }


}
