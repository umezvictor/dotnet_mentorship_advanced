using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.IntegrationTest;
public static class TokenGenerator
{
	public static string GenerateAccessToken()
	{
		var securityKey = "jWnZr4u7x!A%D*G-JaNdRgUkXp2s5v8y";
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

		long epoch = 1764422981L;
		long exp = 1764426581L;

		var claims = new List<Claim>
	{
		new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Manager"),
		new Claim("permission", "Create"),
		new Claim("permission", "Read"),
		new Claim("permission", "Update"),
		new Claim("permission", "Delete"),
		new Claim("scope", "cartApi"),
		new Claim("scope", "permissions"),
		new Claim("scope", "roles"),
		new Claim("scope", "offline_access"),
		new Claim("amr", "pwd"),
		new Claim("client_id", "client"),
		new Claim("sub", "103175ad-2f3c-4453-94a6-69ed5b096552"),
		new Claim("auth_time", epoch.ToString()),
		new Claim("idp", "local"),
		new Claim("jti", "BE8251030056BB93DD5784DAF08BEBBF")
	};

		var token = new JwtSecurityToken(
			issuer: "https://localhost:5001",
			audience: "https://localhost:5001",
			claims: claims,
			notBefore: DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime,
			expires: DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime,
			signingCredentials: creds
		);

		token.Payload["iat"] = epoch;
		token.Payload["nbf"] = epoch;
		token.Payload["exp"] = exp;

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}