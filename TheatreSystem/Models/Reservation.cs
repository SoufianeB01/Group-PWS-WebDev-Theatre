using System;

public class Reservation
{
    public int ReservationID { get; set; }
    public int CustomerID { get; set; }
    public DateTime TheatereShowDate { get; set; }
    public List<Seat> tickets { get; set; }
    public int amountOfTickets { get; set; }
    public bool used { get; set; }
}