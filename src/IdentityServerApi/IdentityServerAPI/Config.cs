using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Shared.Constants;

namespace IdentityServerAPI;

public static class Config
{
	public static IEnumerable<ApiScope> ApiScopes =>
	new[]
	{
		new ApiScope("cartApi", "Cart API")
		{
			UserClaims = { "role", "permission" }
		},
		new ApiScope("catalogApi", "Catalog API")
		{
			UserClaims = { "role", "permission" }
		}
	};
	public static IEnumerable<Client> Clients =>
		new Client[]
		{
			new Client
			{
				ClientId = "client",
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
				ClientSecrets =
				{
					new Secret("secret".Sha256())
				},
				AllowedScopes = {"cartApi", "catalogApi", "offline_access", "roles", "permissions"},
				AllowOfflineAccess = true, //enables refresh tokens, together with the offline_access scope
				AllowedCorsOrigins =
				{
					"http://localhost:5004"
				}
			}
		};

	public static IEnumerable<IdentityResource> IdentityResources => new[]
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Profile(),
		new IdentityResource("roles", "Your role(s)", new[] { "role" }),
		new IdentityResource("permissions", "app permissions", new[] { AppConstants.PermissionClaim })
	};
	public static List<TestUser> Users =>
	new List<TestUser>
	{
			new TestUser
			{
				SubjectId = "103175ad-2f3c-4453-94a6-69ed5b096552",
				Username = "manager",
				Password = "gSb99<:8H1o%",
				Claims = new List<Claim>
				{
					new Claim("role", "Manager"),
					new Claim(AppConstants.PermissionClaim, "Read"),
					new Claim(AppConstants.PermissionClaim, "Create"),
					new Claim(AppConstants.PermissionClaim, "Update"),
					new Claim(AppConstants.PermissionClaim, "Delete")
				}
			},
			new TestUser
			{
				SubjectId = "f1465968-1964-46ec-afbe-8595c89e84b4",
				Username = "customer",
				Password = "FI3WIm22|Q}C",
				Claims = new List<Claim>
				{
					new Claim("role", "StoreCustomer"),
					new Claim(AppConstants.PermissionClaim, "Read"),
					new Claim(AppConstants.PermissionClaim, "Create"),
					new Claim(AppConstants.PermissionClaim, "Delete")
				}
			},
			new TestUser
			{
				SubjectId = "108775ad-2f3c-4453-94a6-69ed5b096142",
				Username = "admin",
				Password = "aSb12<:8H1o%",
				Claims = new List<Claim>
				{
					new Claim("role", "Admin"),
					new Claim(AppConstants.PermissionClaim, "Read"),
					new Claim(AppConstants.PermissionClaim, "Create"),
					new Claim(AppConstants.PermissionClaim, "Update"),
					new Claim(AppConstants.PermissionClaim, "Delete")
				}
			},
	};
}


