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

    [HttpPost("{movieId}")]///{theaterShowDateID}")]
    public async Task<IActionResult> ReserveSeat([FromBody] CustomerReservationRequest customerwithreservation, int movieId)//, int theaterShowDateID)
    {
        if (customerwithreservation == null) return BadRequest("Invalid request");

        var customer = customerwithreservation.Customer;
        var reservation = customerwithreservation.Reservation;

        // Use movieId as needed
        int res = _reservationService.MakeReservation(customerwithreservation, movieId, 0);
        if (res == -1) return BadRequest("Seat is already taken");
        if (res == -2) return BadRequest("Date is in the past");
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