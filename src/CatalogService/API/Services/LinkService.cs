using BLL.Abstractions;
using Shared.Dto;

namespace API.Services;

internal sealed class LinkService (LinkGenerator linkGenerator,
	IHttpContextAccessor httpContextAccessor) : ILinkService
{
	public Link GenerateLinks (string endpointName, object? routeValues, string rel, string method)
	{
		return new Link(
			linkGenerator.GetUriByName( httpContextAccessor.HttpContext,
			endpointName,
			routeValues ),
			rel, method );
	}
}
