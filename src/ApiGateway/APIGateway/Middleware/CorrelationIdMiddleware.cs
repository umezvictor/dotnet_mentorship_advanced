using Shared.Constants;

namespace APIGateway.Middleware;

public sealed class CorrelationIdMiddleware
{
	private readonly RequestDelegate _next;
	public CorrelationIdMiddleware(RequestDelegate next)
	{
		_next = next;
	}
	public async Task Invoke(HttpContext context)
	{
		var correlationId = context.Request.Headers[AppConstants.CorrelationIdHeader].FirstOrDefault() ?? Guid.NewGuid().ToString();
		if (!string.IsNullOrEmpty(correlationId))
		{
			// Set the header on outgoing request to downstream service
			context.Request.Headers.TryAdd(AppConstants.CorrelationIdHeader, correlationId);
		}

		context.Response.OnStarting(() =>
		{
			context.Response.Headers[AppConstants.CorrelationIdHeader] = correlationId;
			return Task.CompletedTask;
		});

		await _next(context);
	}
}

