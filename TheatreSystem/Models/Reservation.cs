using System;

public class Reservation
{
    public int ReservationID { get; set; }
    public int CustomerID { get; set; }
    public TheaterShowDate TheatereShowDate { get; set; }
    public List<Seat> tickets { get; set; }
    public int amountOfTickets { get; set; }
    public bool used { get; set; }
}
//Reservation class is aangepast want THeathershowdte is een class en geen datetime object