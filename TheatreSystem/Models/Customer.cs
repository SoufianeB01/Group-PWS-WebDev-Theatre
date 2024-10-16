public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
public class CustomerReservationRequest
{
    public Customer Customer { get; set; }
    public Reservation Reservation { get; set; }
}
