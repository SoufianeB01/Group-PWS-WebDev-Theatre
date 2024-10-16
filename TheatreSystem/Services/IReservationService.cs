public interface IReservationService
{
    bool ReserveSeat( CustomerReservationRequest request, int movieId);
    List<Reservation> GetReservations();
}