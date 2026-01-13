using System.Diagnostics;
using Shared.Constants;

namespace RabbitMQ;
public static class RabbitMqActivity
{
	public static readonly ActivitySource Source = new ActivitySource(AppConstants.MessageBroker);
}
