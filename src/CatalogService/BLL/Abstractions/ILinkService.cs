using Shared.Dto;

namespace BLL.Abstractions;
public interface ILinkService
{
	Link GenerateLinks (string endpointName, object? routeValues, string rel, string method);
}
