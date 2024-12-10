public interface IReservationService
{
    int MakeReservation( CustomerReservationRequest request, int movieId);
    List<Reservation> GetReservations();
}