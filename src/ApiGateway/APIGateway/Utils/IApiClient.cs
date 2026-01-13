namespace APIGateway.Utils;

public interface IApiClient
{
	Task<string> GetAsync(string url);
}