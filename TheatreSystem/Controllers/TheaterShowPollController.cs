using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TheaterShowPollController : ControllerBase
{
    private readonly TheaterShowPollService _pollService = new TheaterShowPollService();

    [HttpGet]
    public IActionResult GetAllPolls()
    {
        var polls = _pollService.GetAllPolls();
        return Ok(polls);
    }

    [HttpGet("{id}")]
    public IActionResult GetPollById(int id)
    {
        var poll = _pollService.GetPollById(id);
        if (poll == null)
            return NotFound();

        return Ok(poll);
    }

    [HttpPost]
    public IActionResult AddTheaterShowPoll([FromBody] TheaterShowPoll poll)
    {
        try
        {
            _pollService.AddTheaterShowPoll(poll);
            return CreatedAtAction(nameof(GetPollById), new { id = poll.Id }, poll);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{pollId}/vote/{theaterShowIndex}")]
    public IActionResult VoteForShow(int pollId, int theaterShowIndex)
    {
        _pollService.VoteForShow(pollId, theaterShowIndex);
        return Ok();
    }
}
