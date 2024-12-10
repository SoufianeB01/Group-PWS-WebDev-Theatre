using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("reservation")]

public class ReservationController : ControllerBase
{
    private IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost("{movieId}")]
    public async Task<IActionResult> ReserveSeat([FromBody] CustomerReservationRequest customerwithreservation, int movieId)
    {
        if (customerwithreservation == null) return BadRequest("Invalid request");

        var customer = customerwithreservation.Customer;
        var reservation = customerwithreservation.Reservation;

        // Use movieId as needed
        _reservationService.MakeReservation(customerwithreservation, movieId);
        return this.Ok(reservation);
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = _reservationService.GetReservations();
        Console.WriteLine("Reservations: " + reservations.Count);
        return this.Ok(reservations);
    }


}