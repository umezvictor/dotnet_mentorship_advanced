namespace API.IntegrationTest;
public class BaseFunctionalTest : IClassFixture<CatalogWebApplicationFactory>
{
    public BaseFunctionalTest(CatalogWebApplicationFactory factory)
    {
        HttpClient = factory.CreateClient();

    }
    protected HttpClient HttpClient { get; init; }
}