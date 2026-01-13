namespace APIGateway.Utils;

public class ApiClient : IApiClient
{
	private readonly IHttpClientFactory _factory;

	public ApiClient(IHttpClientFactory factory)
	{
		_factory = factory;
	}

	public async Task<string> GetAsync(string url)
	{
		using var client = _factory.CreateClient("swagger");
		var response = await client.GetAsync(url);
		return await response.Content.ReadAsStringAsync();
	}
}

