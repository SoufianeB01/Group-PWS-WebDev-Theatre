using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("movie/seats")]
public class SeatController : ControllerBase
{
    private ISeatService _seatService;

    public SeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpPost("claim")]
    public async Task<IActionResult> ClaimSeat([FromBody] Seat seat)
    {
        if (await _seatService.ClaimSeat(seat))
        {
            return Ok($"Seat at row {seat.Row}, col {seat.Col} claimed");
        }
        return BadRequest("Seat not available");
    }

    [HttpPost("release")]
    public async Task<IActionResult> ReleaseSeat([FromBody] Seat seat)
    {
        if (await _seatService.ReleaseSeat(seat))
        {
            return Ok($"Seat at row {seat.Row}, col {seat.Col} released");
        }
        return BadRequest("Seat not available to release");
    }


[HttpGet("all")]
public IActionResult GetAllSeats()
{
    // Retrieve the seats as a 2D boolean array
    var seatsArray = _seatService.GetAllSeats();
    
    // Convert the bool[,] to a List<List<bool>>
    var result = new List<List<bool>>();
    
    int rows = seatsArray.GetLength(0);
    int cols = seatsArray.GetLength(1);
    
    for (int i = 0; i < rows; i++)
    {
        var rowList = new List<bool>();
        for (int j = 0; j < cols; j++)
        {
            rowList.Add(seatsArray[i, j]); // Populate the row list with the values from the 2D array
        }
        result.Add(rowList); // Add the row list to the result
    }
    
    return Ok(result); // Return the result as a list of lists containing boolean values
}
}
