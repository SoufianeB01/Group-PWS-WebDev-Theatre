using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


    [Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]

public class Reservation_managegerController:ControllerBase
{
    private readonly  AppDbContext _context;

    public ReservationsController(AppDbContext context)
    {
        _context = context;
    }
  


 [HttpGet]
    public async Task<IActionResult> GetReservations([FromQuery] int? showId, [FromQuery] DateTime? date, [FromQuery] string search)
    {
        var reservationsQuery = _context.Reservations.AsQueryable();       
        if (showId.HasValue)
        {
            reservationsQuery = reservationsQuery.Where(x => x.ShowId == showId.Value);
        }
        if (date.HasValue)
        {
            reservationsQuery = reservationsQuery.Where(x => x.ReservationDate.Date == date.Value.Date);
        }
        if (!string.IsNullOrEmpty(search))
        {
            reservationsQuery = reservationsQuery.Where(x => x.Email.Contains(search) || x.ReservationNumber.Contains(search));
        }
        var reservations = await reservationsQuery.ToListAsync();
        return Ok(reservations);
    }

    // PUT: api/Reservations/{id}/mark-as-used
[HttpPut("{id}/mark-as-used")]
    public async Task<IActionResult> MarkAsUsed(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return NotFound();

        reservation.IsUsed = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Reservations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return NotFound();

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}







