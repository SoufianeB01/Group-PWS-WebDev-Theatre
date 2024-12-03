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
    [HttpPost]
    public async Task<IActionResult> ReserveSeats([FromBody] object input,[FromQuery] List<int> movieIds)
    {
    if (input == null)
    {
        return BadRequest("Input cannot be null.");
    }
    var createdShows = new List<CustomerReservationRequest>();
    if (input is List<CustomerReservationRequest> customreservationrequist)
    {
        int count = 0;
        if (!customreservationrequist.Any())
        {
            return BadRequest("The list of requests can't be empty.");
        }
        foreach (var request in customreservationrequist)
        {
            var id = movieIds[count];
            _reservationService.ReserveSeat(request,id);
            count+=1;
        }
    }
    else if (input is Dictionary<string, CustomerReservationRequest> SeatsDict)
    {
        int counte = 0;
        if (!SeatsDict.Any())
        {
            return BadRequest("The dictionary of reequests can't be empty.");
        }
        foreach (var pair in SeatsDict)
        {
            int id = movieIds[counte];
            var request = pair.Value;
            _reservationService.ReserveSeat(request,id);
           
        }
    }
    else
    {
        return BadRequest("Input must be either a list of TheaterShow objects or a dictionary.");
    }
    return this.Ok();
    }
    [HttpGet()]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = _reservationService.GetReservations();
        Console.WriteLine("Reservations: " + reservations.Count);
        return this.Ok(reservations);
    }


}