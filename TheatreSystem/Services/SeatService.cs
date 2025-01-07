using System.Collections.Generic;
using System.Threading.Tasks;

public class SeatDataService : ISeatService
{
    private SeatData _seatData;

    public SeatDataService()
    {
        _seatData = new SeatData();
    }

    // Claim a seat
    public async Task<bool> ClaimSeat(Seat seat)
    {
        if (_seatData.Seats[seat.Row, seat.Col] == false)
            return false;

        _seatData.Seats[seat.Row, seat.Col] = false; // Seat claimed
        return true;
    }

    // Release a seat
    public async Task<bool> ReleaseSeat(Seat seat)
    {
        if (_seatData.Seats[seat.Row, seat.Col] == true)
            return false;

        _seatData.Seats[seat.Row, seat.Col] = true; // Seat released
        return true;
    }

    // Check if a seat is available
    public bool IsSeatAvailable(int row, int col)
    {
        if (row < 0 || row >= 10 || col < 0 || col >= 10)
            return false; //out of range

        return _seatData.Seats[row, col];
    }

    

    // Get the entire seat map
    public bool[,] GetAllSeats()
    {
        return _seatData.Seats;
    }
}
