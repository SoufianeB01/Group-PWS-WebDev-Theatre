using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TheaterShowDateController : ControllerBase
{
    private readonly ITheaterShowDateService _showDateService;

    public TheaterShowDateController(ITheaterShowDateService dateService)
    {
        _showDateService = dateService;
    }

    [HttpGet]
    public ActionResult<List<TheaterShowDate>> GetAllShowDates()
    {
        return _showDateService.GetAllShowDates();
    }

    [HttpGet("{id}")]
    public ActionResult<TheaterShowDate> GetShowDateById(int id)
    {
        var showDate = _showDateService.GetShowDateById(id);
        if (showDate == null)
        {
            return NotFound();
        }
        return showDate;
    }

    [HttpPost]
    public async Task<ActionResult<TheaterShowDate>> CreateShowDate(TheaterShowDate showDate)
    {
        await _showDateService.CreateShowDate(showDate);
        return CreatedAtAction(nameof(GetShowDateById), new { id = showDate.TheaterShowDateID }, showDate);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult<List<TheaterShowDate>>> CreateMultipleShowDates(List<TheaterShowDate> showDates)
    {
        if (showDates == null || showDates.Count == 0)
        {
            return BadRequest("No show dates provided.");
        }

        foreach (var showDate in showDates)
        {
            await _showDateService.CreateShowDate(showDate);
        }

        return CreatedAtAction(nameof(GetAllShowDates), showDates);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShowDate(int id, TheaterShowDate showDate)
    {
        if (id != showDate.TheaterShowDateID)
        {
            return BadRequest();
        }

        await _showDateService.UpdateShowDate(showDate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShowDate(int id)
    {
        var showDate = _showDateService.GetShowDateById(id);
        if (showDate == null)
        {
            return NotFound();
        }

        await _showDateService.DeleteShowDate(id);
        return NoContent();
    }

    [HttpGet("filter")]
    public ActionResult<List<TheaterShowDate>> GetFilteredShowDates(
        [FromQuery] int? id,
        [FromQuery] string date,
        [FromQuery] string time,
        [FromQuery] int? theaterShowId,
        [FromQuery] string sortBy = "date",
        [FromQuery] bool ascending = true)
    {
        return _showDateService.GetFilteredShowDates(id, date, time, theaterShowId, sortBy, ascending);
    }

}