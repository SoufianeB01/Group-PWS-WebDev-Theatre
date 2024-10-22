using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TheaterShowController : ControllerBase
{
    private readonly ITheaterShowService _showService;

    public TheaterShowController(ITheaterShowService showService)
    {
        _showService = showService;
    }

    [HttpGet]
    public IActionResult GetAllShows()
    {
        var shows = _showService.GetAllShows();
        return Ok(shows);
    }
    
   [HttpGet("{id}")]
    public IActionResult GetShowById(int id)
    {
        var show = _showService.GetShowById(id);
        if (show == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }
        return Ok(show);
    }

    [HttpGet("filter")]
    public IActionResult GetFilteredShows(
        int? id,
        string title = null,
        string description = null,
        int? venueId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string sortBy = "title",
        bool ascending = true)
    {
        var shows = _showService.GetFilteredShows(id, title, description, venueId, startDate, endDate, sortBy, ascending);
        return Ok(shows);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShow([FromBody] TheaterShow newShow)
    {
        await _showService.CreateShow(newShow);
        return CreatedAtAction(nameof(GetShowById), new { id = newShow.TheaterShowID }, newShow);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShows([FromBody] object input)
    {
    if (input == null)
    {
        return BadRequest("Input cannot be null.");
    }
    var createdShows = new List<TheaterShow>();
    if (input is List<TheaterShow> listshows)
    {
        if (!listshows.Any())
        {
            return BadRequest("The list of shows can't be empty.");
        }
        foreach (var show in listshows)
        {
            await _showService.CreateShow(show);
            createdShows.Add(show);
        }
    }
    else if (input is Dictionary<string, TheaterShow> ShowsDict)
    {
        if (!ShowsDict.Any())
        {
            return BadRequest("The dictionary of shows can't be empty.");
        }
        foreach (var pair in ShowsDict)
        {
            var show = pair.Value;
            await _showService.CreateShow(show);
            createdShows.Add(show);
        }
    }
    else
    {
        return BadRequest("Input must be either a list of TheaterShow objects or a dictionary.");
    }

    return CreatedAtAction(nameof(GetShowById), new { ids = createdShows.Select(s => s.TheaterShowID).ToArray() }, createdShows);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShow(int id, [FromBody] TheaterShow updatedShow)
    {
        var existingShow = _showService.GetShowById(id);
        if (existingShow == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }

        updatedShow.TheaterShowID = id;
        await _showService.UpdateShow(updatedShow);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShow(int id)
    {
        var existingShow = _showService.GetShowById(id);
        if (existingShow == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }

        await _showService.DeleteShow(id);
        return NoContent();
    }
}