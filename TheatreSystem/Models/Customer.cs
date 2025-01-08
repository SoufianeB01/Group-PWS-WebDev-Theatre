public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Customer(int customerId,string firstName, string lastName, string email)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public Customer()
    {
    }

}
public class CustomerReservationRequest
{
    public Customer Customer { get; set; }
    public Reservation Reservation { get; set; }
}
