using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMQ;

namespace RabbitMQ;
public sealed class RabbitMqClient : IRabbitMqClient
{
	public async Task PublishMessageAsync<T> (T message, string queueName)
	{
		var factory = new ConnectionFactory { HostName = RabbitMQConstants.Host };
		factory.AutomaticRecoveryEnabled = true;
		factory.NetworkRecoveryInterval = TimeSpan.FromSeconds( 10 );
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync( queue: queueName, durable: false, exclusive: false, autoDelete: false,
			arguments: null );

		var json = JsonSerializer.Serialize( message );
		var body = Encoding.UTF8.GetBytes( json );

		await channel.BasicPublishAsync( exchange: string.Empty, routingKey: queueName, body: body );
	}

	public async Task<string> ConsumeMessageAsync (string queueName)
	{
		var factory = new ConnectionFactory { HostName = RabbitMQConstants.Host };
		factory.AutomaticRecoveryEnabled = true;
		factory.NetworkRecoveryInterval = TimeSpan.FromSeconds( 10 );
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync( queue: RabbitMQConstants.ProductQueue, durable: false, exclusive: false, autoDelete: false,
			arguments: null );

		var response = string.Empty;

		var consumer = new AsyncEventingBasicConsumer( channel );
		consumer.ReceivedAsync += async (model, arg) =>
		{
			var body = arg.Body.ToArray();
			var message = Encoding.UTF8.GetString( body );

			try
			{
				response = message;
				await channel.BasicAckAsync( deliveryTag: arg.DeliveryTag, multiple: false );
			}
			catch
			{
				await channel.BasicNackAsync( deliveryTag: arg.DeliveryTag, multiple: false, requeue: true );
			}
			return;
		};
		await channel.BasicConsumeAsync( RabbitMQConstants.ProductQueue, autoAck: false, consumer: consumer );

		return response;
	}
}