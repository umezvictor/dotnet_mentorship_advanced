using System.Text.Json;
using DAL.Database;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Quartz;
using RabbitMQ;
using Shared.Constants;
using Shared.RabbitMQ;

namespace API.Jobs;

[DisallowConcurrentExecution]
public class RabbitMQPublisherJob : IJob
{
	private readonly IApplicationDbContext _dbContext;
	private readonly IRabbitMqClient _rabbitMqClient;

	public RabbitMQPublisherJob(IApplicationDbContext dbContext, IRabbitMqClient rabbitMqClient)
	{
		_dbContext = dbContext;
		_rabbitMqClient = rabbitMqClient;
	}
	public async Task Execute(IJobExecutionContext context)
	{

		var outboxMessages = await _dbContext.Outbox.Where(x => x.Status == OutboxMessageStatus.Failed).ToListAsync();

		if (outboxMessages.Count > 0)
		{
			List<Outbox> processedOutboxMessages = new();
			foreach (var outboxMessage in outboxMessages)
			{
				var productData = JsonSerializer.Deserialize<ProductUpdatedContract>(outboxMessage.Data);
				if (productData != null)
				{
					await _rabbitMqClient.PublishMessageAsync(new ProductUpdatedContract
					{
						Id = productData.Id,
						Name = productData.Name,
						Price = productData.Price
					}, RabbitMQConstants.ProductQueue, outboxMessage.CorrelationId);
				}
				outboxMessage.Status = OutboxMessageStatus.Processed;
				processedOutboxMessages.Add(outboxMessage);
			}

			_dbContext.Outbox.UpdateRange(processedOutboxMessages);
			await _dbContext.SaveChangesAsync();
		}

	}
}

