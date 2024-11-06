using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using RunningGroupAPI.Data;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Helpers.AuthorizationHandler;

public class ClubOwnerOrAdminHandler : IAuthorizationHandler
{
	private readonly IClubService _clubService;

	public ClubOwnerOrAdminHandler(IClubService clubService)
	{
		_clubService = clubService;
	}

	public async Task HandleAsync(AuthorizationHandlerContext context)
	{
		if (context.User.IsInRole(UserRoles.Admin))
		{
			context.Succeed(context.PendingRequirements.First()); // Succeed with the first pending requirement
			return;
		}

		var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? context.User.FindFirstValue("sub");
		if (userId == null)
		{
			context.Fail();
			return;
		}

		// Extract resource ID (club ID) from the route data
		if (context.Resource is AuthorizationFilterContext authContext &&
			authContext.RouteData.Values["id"] is int clubId)
		{
			if (await _clubService.IsClubOwner(userId, clubId))
			{
				context.Succeed(context.PendingRequirements.First());
			}
		}
	}
}