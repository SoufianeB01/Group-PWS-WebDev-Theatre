using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]

public class Reservation_managegerController:ControllerBase
{
    private readonly  List<Reservation> _context;

    public Reservation_managegerController(List<Reservation> context)
    {
        _context = context;
    }
  


 [HttpGet]
    public async Task<IActionResult> GetReservations([FromQuery] int? showId, [FromQuery] DateTime? date, [FromQuery] string search)
    {
        var reservationsQuery = _context.AsQueryable();       
        if (showId.HasValue)
        {
            reservationsQuery = reservationsQuery.Where(x => x.TheatereShowDate.TheaterShowID == showId.Value);
        }
        if (date.HasValue)
        {
            reservationsQuery = reservationsQuery.Where(x => x.TheatereShowDate.Date == date.Value.Date);
        }
        if (!string.IsNullOrEmpty(search))
        {
            
            reservationsQuery = reservationsQuery.Where(x => x.tickets.Any(x =>x.ToString().Contains(search)));
        }
        var reservations = reservationsQuery.ToList();
        return Ok(reservations);
    }

 
[HttpPut("{id}/mark-as-used")]
    public async Task<IActionResult> MarkAsUsed(int id)
    {
        var reservation = _context.Find(x=> x.ReservationID == id);
        if (reservation == null) return NotFound();

        reservation.used = true;
        

        return NoContent();
    }

 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation =_context.Find(x=> x.ReservationID == id);
        if (reservation == null) return NotFound();

        _context.Remove(reservation);
        

        return NoContent();
    }
}







