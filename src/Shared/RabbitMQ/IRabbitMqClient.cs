
namespace RabbitMQ;

public interface IRabbitMqClient
{
	Task<string> ConsumeMessageAsync (string queueName);
	Task PublishMessageAsync<T> (T message, string queueName);
}