namespace API.Middleware;

using System.IdentityModel.Tokens.Jwt;

public sealed class AccessTokenLoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<AccessTokenLoggingMiddleware> _logger;

	public AccessTokenLoggingMiddleware (RequestDelegate next, ILogger<AccessTokenLoggingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	/// <summary>
	/// logs details from the access token present in the Authorization header
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public async Task Invoke (HttpContext context)
	{
		var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
		if (!string.IsNullOrEmpty( authHeader ) && authHeader.StartsWith( "Bearer ",
			StringComparison.OrdinalIgnoreCase ))
		{
			var token = authHeader.Substring( "Bearer ".Length ).Trim();
			try
			{
				var handler = new JwtSecurityTokenHandler();
				var jwt = handler.ReadJwtToken( token );

				var sub = jwt.Claims.FirstOrDefault( c => c.Type == "sub" )?.Value;
				var clientId = jwt.Claims.FirstOrDefault( c => c.Type == "client_id" )?.Value;
				var role = jwt.Claims.FirstOrDefault( c => c.Type == "role"
				|| c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" )?.Value;
				var permissions = jwt.Claims.Where( c => c.Type == "permission" ).Select( c => c.Value ).ToArray();

				_logger.LogInformation(
					"AccessToken Details: sub = {Sub}, client_id = {ClientId}, role = {Role}, permissions = [{Permissions}]",
					sub, clientId, role, string.Join( ", ", permissions )
				);
			}
			catch (Exception ex)
			{
				_logger.LogError( ex, "Failed to decode JWT access token." );
			}
		}

		await _next( context );
	}
}
