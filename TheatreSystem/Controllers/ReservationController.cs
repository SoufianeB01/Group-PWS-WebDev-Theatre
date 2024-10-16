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

    [HttpPost()]
    public async Task<IActionResult> ReserveSeat([FromBody] CustomerReservationRequest customerwithreservation, [FromQuery] int movieId)
    {
        if (customerwithreservation == null) return BadRequest("Invalid request");

        var customer = customerwithreservation.Customer;
        var reservation = customerwithreservation.Reservation;

        // Use movieId as needed
        Console.WriteLine("Movie ID: " + movieId);
        Console.WriteLine("Customer ID: " + reservation.CustomerID);
        _reservationService.ReserveSeat(customerwithreservation, movieId);
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