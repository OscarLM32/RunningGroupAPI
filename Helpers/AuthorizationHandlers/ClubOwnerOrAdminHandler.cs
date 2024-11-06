using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using RunningGroupAPI.Data;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Helpers.AuthorizationHandler;

public class ClubOwnerOrAdminRequirement : IAuthorizationRequirement{}

public class ClubOwnerOrAdminHandler : AuthorizationHandler<ClubOwnerOrAdminRequirement>
{
	private readonly IClubService _clubService;

	public ClubOwnerOrAdminHandler(IClubService clubService)
	{
		_clubService = clubService;
	}

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClubOwnerOrAdminRequirement requirement)
    {
		if (!context.User.Identity?.IsAuthenticated ?? false)
		{
			context.Fail();
			return;
		}

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


		var authContext = context.Resource as DefaultHttpContext;
		int clubId = int.Parse((string)authContext?.GetRouteData().Values["id"]);

		// Extract resource ID (club ID) from the route data
		if (clubId != null)
		{
			if (await _clubService.IsClubOwner(userId, clubId))
			{
				context.Succeed(context.PendingRequirements.FirstOrDefault());
				return;
			}
		}

		context.Fail();
	}
}