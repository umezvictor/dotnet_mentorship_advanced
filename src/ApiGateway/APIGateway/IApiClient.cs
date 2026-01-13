
namespace APIGateway;

public interface IApiClient
{
	Task<string> GetAsync(string url);
}