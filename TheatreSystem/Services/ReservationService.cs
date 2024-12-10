public class ReservationService : IReservationService
{
private readonly AppDbContext _context;

    public ReservationService(AppDbContext context)
    {
        _context = context;
    }


    public int MakeReservation(CustomerReservationRequest request, int movieId)
    {
        var customer = request.Customer;
        var reservation = request.Reservation;
        
        int reservationId = _context.Reservations.Count() + 1;
        int customerId = customer.CustomerId;
        TheaterShowDate dateTime = reservation.TheatereShowDate;
        int amountOfTickets = reservation.amountOfTickets;
        bool used = false;
        // check if seats are taken
        // check if date is not in the past

        _context.Reservations.Add(new Reservation(reservationId, customerId, dateTime, new List<Seat>(), amountOfTickets, used));
        _context.SaveChanges(); // Persist changes to the database
        Console.WriteLine($"Reservations count after adding: {_context.Reservations.Count()}");  // Check count here

        // return total price
        
        return 0;
    }


    public List<Reservation> GetReservations()
    {
        return _context.Reservations.ToList();
    }

}