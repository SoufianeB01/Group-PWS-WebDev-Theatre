using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft;


[Route($"api/admin"),]


public class Reservation_managegerController:ControllerBase
{
    private readonly  List<Reservation> _context;
    private ISeatService _seatService;
    public Reservation_managegerController(List<Reservation> context,ISeatService seatService)
    {
        _context = context;
        _seatService = seatService;
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
            reservationsQuery = reservationsQuery.Where(x => DateTime.Parse(x.TheatereShowDate.Date) == date.Value.Date);
        }
        if (!string.IsNullOrEmpty(search))
        {
            
            reservationsQuery = reservationsQuery.Where(x => x.tickets.Any(x =>x.ToString().Contains(search)));
        }
        var reservations = reservationsQuery.ToList();
        return Ok(reservations);
    }

 
    // [HttpPut("{id}/mark-as-used")]
    // public async Task<IActionResult> MarkAsUsed(Seat seat)
    // {
        
        
    //     if (seat == null) return NotFound();
    //     else
    //     {
    //         var preneeded = new SeatController(_seatService);
    //         var needed = await preneeded.ClaimSeat(seat);
        

    //     return NoContent();
    //     }
    // }

 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation =_context.Find(x=> x.ReservationID == id);
        if (reservation == null) return NotFound();

        _context.Remove(reservation);
        

        return NoContent();
    }
}







