using System.Security.Claims;

namespace RunningGroupAPI.Helpers.Extensions;

public static class HttpContextAccessorExtensions
{
	public static string GetUserId(this IHttpContextAccessor context)
	{
		return context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}