using Microsoft.AspNetCore.Mvc;
using RunningGroupAPI.Interfaces.Services;

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