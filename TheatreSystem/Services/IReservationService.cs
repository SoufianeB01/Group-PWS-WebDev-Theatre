
public interface IReservationService
{
    float MakeReservation( CustomerReservationRequest request, int movieId, int theaterShowDateID);
    List<Reservation> GetReservations();
    List<Reservation> GetReservationsInShoppingCart();
    Task Checkout();
}