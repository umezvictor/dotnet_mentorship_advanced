using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.IntegrationTests;
public static class TokenGenerator
{
	public static string GenerateAccessToken ()
	{

		var securityKey = "jWnZr4u7x!A%D*G-JaNdRgUkXp2s5v8y";
		var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( securityKey! ) );
		var creds = new SigningCredentials( key, SecurityAlgorithms.HmacSha256Signature );

		long epoch = 1764422981L;
		long exp = 1764426581L;

		var payload = new JwtPayload
	{
		{ "iss", "https://localhost:5001" },
		{ "nbf", epoch },
		{ "iat", epoch },
		{ "exp", exp },
		{ "scope", new[] { "cartApi", "permissions", "roles", "offline_access" } },
		{ "amr", new[] { "pwd" } },
		{ "client_id", "client" },
		{ "sub", "103175ad-2f3c-4453-94a6-69ed5b096552" },
		{ "auth_time", epoch },
		{ "idp", "local" },
		{ "role", "Manager" },
		{ "permission", new[] { "Read", "Create", "Update", "Delete" } },
		{ "jti", "BE8251030056BB93DD5784DAF08BEBBF" }
	};

		var header = new JwtHeader( creds );

		var token = new JwtSecurityToken( header, payload );
		return new JwtSecurityTokenHandler().WriteToken( token );

	}
}