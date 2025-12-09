using System.Text;
using CatalogService;
using DAL.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace API.IntegrationTest;

public class CatalogWebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost (IWebHostBuilder builder)
	{
		builder.ConfigureTestServices( services =>
		{
			services.RemoveAll( typeof( DbContextOptions<ApplicationDbContext> ) );
			services.AddDbContext<ApplicationDbContext>( options =>
				options.UseInMemoryDatabase( "TestDb" )
			);



			services.PostConfigureAll<JwtBearerOptions>( options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = "https://localhost:5001",
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes( "jWnZr4u7x!A%D*G-JaNdRgUkXp2s5v8y" )
					),
					ValidateLifetime = false,
					RoleClaimType = "role",
					NameClaimType = "sub"
				};
			} );
		} );
	}
}
