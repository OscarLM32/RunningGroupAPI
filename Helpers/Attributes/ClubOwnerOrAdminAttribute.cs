using Microsoft.AspNetCore.Authorization;

namespace RunningGroupAPI.Helpers.Attributes;

public class ClubOwnerOrAdminAttribute : AuthorizeAttribute
{
	public ClubOwnerOrAdminAttribute() : base ("ClubOwnerOrAdminPolicy")
	{
		
	}
}