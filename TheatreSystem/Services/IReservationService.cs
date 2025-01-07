public interface IReservationService
{
    int MakeReservation( CustomerReservationRequest request, int movieId, int theaterShowDateID);
    List<Reservation> GetReservations();
    
}