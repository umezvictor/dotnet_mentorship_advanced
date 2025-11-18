using BLL.Services;
using Quartz;
using RabbitMQ;
using Shared.RabbitMQ;
using System.Text.Json;

namespace API.Jobs;



[DisallowConcurrentExecution]
public class RabbitMqListenerJob : IJob
{
    private readonly IRabbitMqClient _rabbitMqClient;
    private readonly ICartService _cartService;

    public RabbitMqListenerJob(IRabbitMqClient rabbitMqClient, ICartService cartService)
    {
        _rabbitMqClient = rabbitMqClient;
        _cartService = cartService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        var message = await _rabbitMqClient.ConsumeMessageAsync(RabbitMQConstants.ProductQueue);
        if (!string.IsNullOrEmpty(message))
        {
            ProductUpdatedContract? productUpdate = null;
            productUpdate = JsonSerializer.Deserialize<ProductUpdatedContract>(message);
            if (productUpdate != null)
            {
                await _cartService.UpdateCartItemsFromMessageConsumerAsync(productUpdate, CancellationToken.None);
            }

        }

    }
}
