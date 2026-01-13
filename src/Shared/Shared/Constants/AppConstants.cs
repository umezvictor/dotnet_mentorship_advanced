namespace Shared.Constants;
public static class AppConstants
{
	public const string CorsPolicy = "CorsPolicy";
	public const string AdminPolicy = "AdminPolicy";
	public const string CustomerPolicy = "CustomerPolicy";
	public const string AuthenticatedUserPolicy = "AuthenticatedUserPolicy";
	public const string RateLimitingPolicy = "RateLimitingPolicy";
	public const string PermissionClaim = "permission";
	public const string MessageBroker = "RabbitMQ";
	public const string CorrelationIdHeader = "X-Correlation-ID";
	public const string CorrelationIdTag = "correlation_id";
}