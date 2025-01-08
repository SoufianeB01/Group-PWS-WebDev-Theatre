public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;
    private readonly ISeatService _seatService;
    private readonly ReservationData _shoppingCart;

    public ReservationService(AppDbContext context, ReservationData shoppingCart)
    {
        _context = context;
        _seatService = new SeatDataService(context);
        _shoppingCart = shoppingCart;
    }


    public float MakeReservation(CustomerReservationRequest request, int movieId, int theaterShowDateID)
    {
        var customer = request.Customer;
        var reservation = request.Reservation;

        int customerId = CustomerAdder(customer);
        //get TheaterShowDate from database from id
        TheaterShowDate theaterShowDate = GetTheaterShowDate(theaterShowDateID);
        Console.WriteLine(theaterShowDateID);
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
        bool isDateInPast = IsDateInPast(theaterShowDate.Date, theaterShowDate.Time);
        if (isDateInPast)
        {
            return -2;
        }



        // add to shopping cart
        _shoppingCart.AddReservationToShoppingCart(new Reservation(reservationId, customerId, theaterShowDate, tickets, amountOfTickets, used));

        //get theathre from theaterShowDateID
        // var theatre = _context.Theaters.FirstOrDefault(t => t.TheaterID == theaterShowDate.TheaterID);

        // return total price
        return GetTotalPrice(amountOfTickets, theaterShowDateID);
    }

    public float GetTotalPrice(int amountOfTickets, int theaterShowDateID)
    {
        //get TheaterShowDate from database from id
        TheaterShowDate theaterShowDate = GetTheaterShowDate(theaterShowDateID);
        Console.WriteLine(theaterShowDate);
        try
        {
            var show = _context.TheaterShows.FirstOrDefault(t => t.TheaterShowID == theaterShowDate.TheaterShowID);
            float price = show.Price;
            return amountOfTickets * price;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }


    public List<Reservation> GetReservations()
    {
        return _context.Reservations.ToList();
    }

    public List<Reservation> GetReservationsInShoppingCart()
    {
        return _shoppingCart.Reservations.Values.ToList();
    }

    public async Task Checkout()
    {
        foreach (Reservation reservation in _shoppingCart.Reservations.Values)
        {
            // Attach the existing TheaterShowDate to avoid adding it as a new entity
            var existingTheaterShowDate = _context.TheaterShowDates
                .FirstOrDefault(t => t.TheaterShowDateID == reservation.TheatereShowDate.TheaterShowDateID);

            if (existingTheaterShowDate != null)
            {
                _context.Attach(existingTheaterShowDate);
            }

            var newReservation = new Reservation
            {
                CustomerID = reservation.CustomerID,
                TheatereShowDate = existingTheaterShowDate, 
                tickets = reservation.tickets,
                amountOfTickets = reservation.amountOfTickets,
                used = reservation.used
            };
            
            //claim the seats
            foreach (Seat ticket in reservation.tickets)
            {
                _seatService.ClaimSeat(ticket, existingTheaterShowDate.TheaterShowDateID);
            }


            _context.Reservations.Add(newReservation);
        }

        // Clear the shopping cart
        _shoppingCart.Reservations.Clear();

        await _context.SaveChangesAsync();
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
            // Console.WriteLine(theaterShowDate.TheaterShowDateID);
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
            Console.WriteLine($"Parsed DateTime: {showDateTime}");
            Console.WriteLine($"Current DateTime: {DateTime.Now}");

            // Check if the date is in the past
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

