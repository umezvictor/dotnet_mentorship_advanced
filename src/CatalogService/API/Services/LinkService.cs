using BLL.Abstractions;
using Shared.Dto;

namespace API.Services;

internal sealed class LinkService (LinkGenerator linkGenerator,
	IHttpContextAccessor httpContextAccessor) : ILinkService
{
	public Link GenerateLinks (string endpointName, object? routeValues, string rel, string method)
	{
		var httpContext = httpContextAccessor.HttpContext;
		var href = httpContext != null
			? linkGenerator.GetUriByName( httpContext, endpointName, routeValues )
			: null;

		return new Link( href ?? string.Empty, rel, method );
	}
}
