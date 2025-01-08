using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISeatService
{
    Task<bool> ClaimSeat(Seat seat, int movieDateId);              // Claim a specific seat
    void ClaimSeats(List<Seat> seat, int movieDateId); // Claim multiple seats
    Task<bool> ReleaseSeat(Seat seat);            // Release a specific seat
    bool IsSeatAvailable(int row, int col); // Check if a seat is available
    bool[,] GetAllSeats();                         // Get the full seating arrangement
}
