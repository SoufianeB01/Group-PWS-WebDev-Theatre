public class ReservationData
{
    public Dictionary<int, Reservation> Reservations { get; set; }
    private int counter;

    public ReservationData()
    {
        Reservations = new Dictionary<int, Reservation>();
    }

    public void AddReservationToShoppingCart(Reservation reservation)
    {
        Reservations.Add(counter++, reservation);
    }


}