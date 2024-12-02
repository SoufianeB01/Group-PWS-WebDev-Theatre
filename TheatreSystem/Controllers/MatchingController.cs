using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheatreSystem.Services;

[ApiController]
[Route("api/match")]
public class MatchController : ControllerBase
{
    private readonly MatchingService _matchingService;

    public MatchController(MatchingService matchingService)
    {
        _matchingService = matchingService;
    }


    [HttpPost]
    public IActionResult MatchUsers([FromBody] List<TheatreSystem.Models.User> users)
    {
        if (users == null || users.Count < 2)
        {
            return BadRequest("At least two users are required to find matches.");
        }

        var matches = _matchingService.FindMatches(users);
        return Ok(matches);
    }
}
