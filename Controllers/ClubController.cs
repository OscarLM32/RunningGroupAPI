using Microsoft.AspNetCore.Mvc;

namespace RunningGroupAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClubController : ControllerBase
{
	private readonly IClubService _clubService;

    public ClubController(IClubService clubService)
    {
        _clubService = clubService;
    }
}