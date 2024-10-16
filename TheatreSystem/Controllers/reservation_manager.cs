using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationsController(AppDbContext context) => _context = context;

    // GET: api/Reservations
    [HttpGet]
    public async Task<IActionResult> GetReservations([FromQuery] int? showId, [FromQuery] DateTime? date, [FromQuery] string search)
    {
        var reservations = await _context.Reservations
            .Include(r => r.Show)
            .Where(r => (!showId.HasValue || r.ShowId == showId) &&
                        (!date.HasValue || r.ReservationDate.Date == date.Value.Date) &&
                        (string.IsNullOrEmpty(search) || r.Email.Contains(search) || r.ReservationNumber.Contains(search)))
            .ToListAsync();

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
