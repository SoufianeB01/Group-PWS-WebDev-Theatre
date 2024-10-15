
using System.Linq;

namespace Space1_5
{
    


public class Reservation_managegerController{
  
  public List<Reservation> reservations{get;set;}
  public List<Customer>customers{get;set;}
  public Customer find_customer_email(int id)
  {
    var customer = customers.First(r=>r.CustomerId == id);
    return customer;
    
  }
  public void  SearchReservations(string email, int Reservationid)
    {
     


        if (!string.IsNullOrEmpty(email))
        {
            var customer = find_customer_email(Reservationid);
            reservations = reservations.Where(r => r.CustomerID==customer.CustomerId).ToList();
        }

        if (!string.IsNullOrEmpty(Reservationid.ToString()))
        {
            reservations = reservations.Where(r => r.ReservationID==Reservationid).ToList();
        }

        if (reservations.Count() == 0)
        {
             Console.WriteLine("No matching reservations found.");
        }


        return Ok(reservations);
    }
    public void GetReservations(TheaterShow show, DateTime? date)
    {
        

      
    

      
        if (date.HasValue)
        {
            reservations = reservations.Where(r => r.TheatereShowDate.Date == date.Value.Date).ToList();
        }

        return Ok(reservations);
    }

}
}