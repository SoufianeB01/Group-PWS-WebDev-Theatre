using System;

public class Reservation
{
    public int ReservationID { get; set; }
    public int CustomerID { get; set; }
    public TheaterShowDate TheatereShowDate { get; set; }
    public List<Seat> tickets { get; set; }
    public int amountOfTickets { get; set; }
    public bool used { get; set; }

    public Reservation() { }

    public Reservation(int ReservationID, int CustomerID, TheaterShowDate TheatereShowDate, List<Seat> tickets, int amountOfTickets, bool used)
    {
        this.ReservationID = ReservationID;
        this.CustomerID = CustomerID;
        this.TheatereShowDate = TheatereShowDate;
        this.tickets = tickets;
        this.amountOfTickets = amountOfTickets;
        this.used = used;
    }
}