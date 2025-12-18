using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

/// <summary>
/// Controller for retrieving the current user's identity claims.
/// </summary>
[Route("identity")]
[Authorize]
public class IdentityController : ControllerBase
{
	/// <summary>
	/// Gets the claims for the currently authenticated user.
	/// </summary>
	/// <returns>A JSON array of claim types and values.</returns>
	[HttpGet]
	public IActionResult Get()
	{
		return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
	}
}