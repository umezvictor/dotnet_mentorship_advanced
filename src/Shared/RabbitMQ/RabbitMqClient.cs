using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMQ;

namespace RabbitMQ;
public sealed class RabbitMqClient : IRabbitMqClient
{
	public async Task PublishMessageAsync<T>(T message, string queueName, string correlationId)
	{
		var factory = new ConnectionFactory { HostName = RabbitMQConstants.Host, UserName = RabbitMQConstants.Username, Password = RabbitMQConstants.Password };
		factory.AutomaticRecoveryEnabled = true;
		factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
			arguments: null);

		using var activity = RabbitMqActivity.Source.StartActivity("Publish Message", ActivityKind.Producer);
		activity?.SetTag("message broker", "RabbitMQ");
		activity?.SetTag("destination queue", queueName);
		activity?.SetTag("correlation_id", correlationId);

		var json = JsonSerializer.Serialize(message);
		var body = Encoding.UTF8.GetBytes(json);

		var props = new BasicProperties();
		props.CorrelationId = correlationId;

		await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, true, props, body: body);

		activity?.AddEvent(new ActivityEvent("Message Published"));
	}

	public async Task<string> ConsumeMessageAsync(string queueName)
	{
		var factory = new ConnectionFactory { HostName = RabbitMQConstants.Host, UserName = RabbitMQConstants.Username, Password = RabbitMQConstants.Password };
		factory.AutomaticRecoveryEnabled = true;
		factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync(queue: RabbitMQConstants.ProductQueue, durable: false, exclusive: false, autoDelete: false,
			arguments: null);

		using var activity = RabbitMqActivity.Source.StartActivity("Consume Message", ActivityKind.Consumer);


		var correlationId = string.Empty;
		var response = string.Empty;
		var consumer = new AsyncEventingBasicConsumer(channel);
		consumer.ReceivedAsync += async (model, arg) =>
		{
			var props = arg.BasicProperties;
			if (!string.IsNullOrEmpty(props?.CorrelationId))
			{
				correlationId = props.CorrelationId;
			}
			var body = arg.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			try
			{
				response = message;
				await channel.BasicAckAsync(deliveryTag: arg.DeliveryTag, multiple: false);
			}
			catch
			{
				await channel.BasicNackAsync(deliveryTag: arg.DeliveryTag, multiple: false, requeue: true);
			}
			return;
		};
		await channel.BasicConsumeAsync(RabbitMQConstants.ProductQueue, autoAck: false, consumer: consumer);
		activity?.SetTag("correlation_id", correlationId);
		activity?.SetTag("message broker", "RabbitMQ");
		activity?.SetTag("source queue", queueName);
		activity?.AddEvent(new ActivityEvent("Message Consumed"));

		return response;
	}
}