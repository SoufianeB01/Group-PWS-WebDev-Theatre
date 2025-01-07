using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISeatService
{
    Task<bool> ClaimSeat(Seat seat);              // Claim a specific seat
    Task<bool> ReleaseSeat(Seat seat);            // Release a specific seat
    bool IsSeatAvailable(int row, int col); // Check if a seat is available
    bool[,] GetAllSeats();                         // Get the full seating arrangement
}
