using Shared.Constants;

namespace API.Middleware;

public sealed class CorrelationIdMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<CorrelationIdMiddleware> _logger;

	public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}
	public async Task Invoke(HttpContext context)
	{
		var correlationId = context.Request.Headers[AppConstants.CorrelationIdHeader].FirstOrDefault();
		if (!string.IsNullOrEmpty(correlationId))
		{

			_logger.LogInformation("Correlation ID {CorrelationId}", correlationId);
		}

		await _next(context);
	}
}
