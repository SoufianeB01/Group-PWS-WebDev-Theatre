public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;
    private readonly ISeatService _seatService;
    private ReservationData ShoppingCart;

    public ReservationService(AppDbContext context)
    {
        _context = context;
        _seatService = new SeatDataService();
        ShoppingCart = new ReservationData();

    }


    public int MakeReservation(CustomerReservationRequest request, int movieId, int theaterShowDateID)
    {
        var customer = request.Customer;
        var reservation = request.Reservation;

        int customerId = CustomerAdder(customer);
        //get TheaterShowDate from database from id
        TheaterShowDate theaterShowDate = GetTheaterShowDate(theaterShowDateID);

        int reservationId = _context.Reservations.Count() + 1;
        int amountOfTickets = reservation.amountOfTickets;
        bool used = false;
        // check if seats are taken
        var tickets = request.Reservation.tickets;
        foreach (Seat ticket in tickets)
        {
            if (!_seatService.IsSeatAvailable(ticket.Row, ticket.Col))
            {
                return -1;
            }
        }
        // check if date is not in the past
        // if (IsDateInPast(theaterShowDate.Date, theaterShowDate.Time))
        // {
        //     return -2;
        // }
        //get theathre from theaterShowDateID
        // var theatre = _context.Theaters.FirstOrDefault(t => t.TheaterID == theaterShowDate.TheaterID);


        // add to shopping cart
        ShoppingCart.Reservations.Add(new Reservation(reservationId, customerId, theaterShowDate, tickets, amountOfTickets, used));
        // _context.Reservations.Add(new Reservation(reservationId, customerId, dateTime, new List<Seat>(), amountOfTickets, used));
        // _context.SaveChanges(); // Persist changes to the database
        Console.WriteLine($"Reservations count after adding: {_context.Reservations.Count()}");  // Check count here

    
        // return total price
        
        return 0;
    }


    public List<Reservation> GetReservations()
    {
        return _context.Reservations.ToList();
    }


    public int CustomerAdder(Customer customer)
    {
        List<Customer> customers = _context.Customers.ToList();
        bool customerExists = false;
        foreach (Customer c in customers)
        {
            if (c.FirstName == customer.FirstName && c.LastName == customer.LastName && c.Email == customer.Email)
            {
                customerExists = true;
                return c.CustomerId;
            }
        }

        int customerId = _context.Customers.Count() + 1;
        _context.Customers.Add(new Customer(customerId, customer.FirstName, customer.LastName, customer.Email));
        _context.SaveChanges(); // Persist changes to the database
        return customerId;

    }

    public TheaterShowDate GetTheaterShowDate(int theaterShowDateID)
    {
        List<TheaterShowDate> theaterShowDates = _context.TheaterShowDates.ToList();
        foreach (TheaterShowDate theaterShowDate in theaterShowDates)
        {
            if (theaterShowDate.TheaterShowDateID == theaterShowDateID)
            {
                return theaterShowDate;
            }
        }
        return null;
    }

    public bool IsDateInPast(string date, string time)
    {
        try
        {
            DateTime showDateTime = DateTime.Parse($"{date} {time}");

            if (showDateTime < DateTime.Now)
            {
                return true;
            }

            return false;
        }
        catch (FormatException)
        {
            throw new Exception("Invalid date or time format.");
        }
    }
}

