using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("reservation")]

public class ReservationController : ControllerBase
{
    // private ReservationService _reservationService;

    // public ReservationController(ReservationService reservationService)
    // {
    //     _reservationService = reservationService;
    // }

    [HttpPost()]
    public async Task<IActionResult> ReserveSeat([FromBody] CustomerReservationRequest  customerwithreservation)
    {
        if(customerwithreservation == null) return BadRequest("Invalid request");
        var customer = customerwithreservation.Customer;
        var reservation = customerwithreservation.Reservation;
        Console.WriteLine(reservation.CustomerID);
        return this.Ok(customer);
    }

}