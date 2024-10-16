public class ReservationService : IReservationService
{
    private ReservationData _reservationData;

    public ReservationService(ReservationData reservationData)
    {
        _reservationData = reservationData;
    }

public bool ReserveSeat(CustomerReservationRequest request, int movieId)
{
    var customer = request.Customer;
    var reservation = request.Reservation;
    
    Console.WriteLine($"Customer ID: {customer.CustomerId}");
    Console.WriteLine($"Reservation Date: {reservation.TheatereShowDate}");
    
    int reservationId = _reservationData.Reservations.Count + 1;
    int customerId = customer.CustomerId;
    TheaterShowDate dateTime = reservation.TheatereShowDate;
    int amountOfTickets = reservation.amountOfTickets;
    bool used = false;
    
    _reservationData.Reservations.Add(new Reservation(reservationId, customerId, dateTime, new List<Seat>(), amountOfTickets, used));
    
    Console.WriteLine($"Reservations count after adding: {_reservationData.Reservations.Count}");  // Check count here
    return true;
}


    public List<Reservation> GetReservations()
    {
        return _reservationData.Reservations;
    }
}