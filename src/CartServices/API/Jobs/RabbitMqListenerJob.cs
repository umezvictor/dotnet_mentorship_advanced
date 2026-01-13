using System.Text.Json;
using BLL.Services;
using Quartz;
using RabbitMQ;
using Shared.RabbitMQ;

namespace API.Jobs
{
	/// <summary>
	/// Quartz job that listens to RabbitMQ for product update messages and updates cart items accordingly.
	/// </summary>
	[DisallowConcurrentExecution]
	public class RabbitMqListenerJob : IJob
	{
		private readonly IRabbitMqClient _rabbitMqClient;
		private readonly ICartService _cartService;

		/// <summary>
		/// Initializes a new instance of the <see cref="RabbitMqListenerJob"/> class.
		/// </summary>
		/// <param name="rabbitMqClient">The RabbitMQ client used to consume messages.</param>
		/// <param name="cartService">The cart service used to update cart items.</param>
		public RabbitMqListenerJob(IRabbitMqClient rabbitMqClient, ICartService cartService)
		{
			_rabbitMqClient = rabbitMqClient;
			_cartService = cartService;
		}

		/// <summary>
		/// Executes the job to consume product update messages from RabbitMQ and update cart items.
		/// </summary>
		/// <param name="context">The job execution context.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
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
}
