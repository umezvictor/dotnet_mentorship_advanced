namespace API.IntegrationTest;
public class BaseFunctionalTest : IClassFixture<CatalogWebApplicationFactory>
{
	public BaseFunctionalTest(CatalogWebApplicationFactory factory)
	{
		var token = TokenGenerator.GenerateAccessToken();
		HttpClient = factory.CreateClient();
		HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
	}
	protected HttpClient HttpClient { get; init; }
}