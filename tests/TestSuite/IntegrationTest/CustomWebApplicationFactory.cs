using CartService;
using CartService.BLL.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using Moq;

namespace TestSuite.IntegrationTest;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbRunner _mongoRunner;

    public CustomWebApplicationFactory(MongoDbRunner mongoRunner)
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
            config.AddInMemoryCollection(inMemorySettings);
        });

        builder.ConfigureServices(services =>
        {
            // Remove all existing registrations of ICartRepository
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(ICartRepository)
            );
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            //repository mock
            var mockRepo = new Mock<ICartRepository>();
            mockRepo.Setup(x => x.GetItemsAsync()).ReturnsAsync(TestData.CartItems);
            mockRepo.Setup(x => x.AddItemAsync(TestData.CartItem()));
            mockRepo.Setup(x => x.RemoveItemAsync(It.IsAny<int>())).ReturnsAsync(true);
            services.AddSingleton(mockRepo.Object);
        });
    }


}
