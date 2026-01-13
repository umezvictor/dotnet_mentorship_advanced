using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace API.Infrastructure;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
   : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
	HttpContext httpContext,
	Exception exception,
	CancellationToken cancellationToken)
	{
		var correlationId = httpContext.Request.Headers[AppConstants.CorrelationIdHeader].FirstOrDefault();
		logger.LogError(exception, $"An exception occurred. Correlation ID: {correlationId}");
		var problemDetails = new ProblemDetails
		{
			Status = exception switch
			{
				ArgumentException => StatusCodes.Status400BadRequest,
				_ => StatusCodes.Status500InternalServerError
			},
			Title = "An error occurred",
			Type = exception.GetType().Name,
			Detail = exception.Message
		};

		return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			Exception = exception,
			HttpContext = httpContext,
			ProblemDetails = problemDetails
		});
	}
}