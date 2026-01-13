using Serilog;

namespace IdentityServerAPI;

internal static class HostingExtensions
{
	public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
	{

		builder.Services.AddIdentityServer(options =>
		{
			//Todo: read from configuration
			options.IssuerUri = "http://identityserverapi:8080";
		})
			.AddInMemoryApiScopes(Config.ApiScopes)
			.AddInMemoryClients(Config.Clients)
			.AddTestUsers(Config.Users)
		.AddInMemoryIdentityResources(Config.IdentityResources);

		return builder.Build();
	}

	public static WebApplication ConfigurePipeline(this WebApplication app)
	{
		app.UseSerilogRequestLogging();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseIdentityServer();
		return app;
	}
}