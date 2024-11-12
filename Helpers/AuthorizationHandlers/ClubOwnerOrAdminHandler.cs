using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using RunningGroupAPI.Data;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Helpers.AuthorizationHandler;

public class ClubOwnerOrAdminRequirement : IAuthorizationRequirement{}

public class ClubOwnerOrAdminHandler : AuthorizationHandler<ClubOwnerOrAdminRequirement>
{
	private readonly IClubMembershipService _clubMembershipService;

	public ClubOwnerOrAdminHandler(IClubMembershipService clubMembershipService)
	{
		_clubMembershipService = clubMembershipService;
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
		string clubId = (string)authContext?.GetRouteData().Values["id"] ?? string.Empty;

		if (!clubId.IsNullOrEmpty())
		{
			if(await _clubMembershipService.IsOwner(clubId, userId))
			{
				context.Succeed(context.PendingRequirements.FirstOrDefault());
				return;
			}
		}

		context.Fail();
	}
}