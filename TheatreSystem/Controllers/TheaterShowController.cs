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

    // GET: api/TheaterShow
    [HttpGet]
    public IActionResult GetAllShows()
    {
        var shows = _showService.GetAllShows();
        return Ok(shows);
    }

    // GET: api/TheaterShow/{id}
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

    // POST: api/TheaterShow
    [HttpPost]
    public async Task<IActionResult> CreateShow([FromBody] TheaterShow newShow)
    {
        await _showService.CreateShow(newShow);
        return CreatedAtAction(nameof(GetShowById), new { id = newShow.TheaterShowID }, newShow);
    }

    // PUT: api/TheaterShow/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShow(int id, [FromBody] TheaterShow updatedShow)
    {
        var existingShow = _showService.GetShowById(id);
        if (existingShow == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }

        updatedShow.TheaterShowID = id; // Ensure correct ID
        await _showService.UpdateShow(updatedShow);
        return NoContent();
    }

    // DELETE: api/TheaterShow/{id}
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
