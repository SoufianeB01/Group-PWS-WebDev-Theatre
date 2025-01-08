using System.Collections.Generic;
using System.Threading.Tasks;

public class SeatDataService : ISeatService
{
    public AppDbContext _context { get; set; }
    private SeatData _seatData;

    public SeatDataService(AppDbContext context)
    {
        _context = context;
        _seatData = new SeatData();
    }


    // Claim a seat
    public async Task<bool> ClaimSeat(Seat seat, int movieDateId)
    {
        
        // Get seating plan for the specified movie date
        var seatingPlan = _context.SeatingPlan.FirstOrDefault(s => s.TheaterShowDateID == movieDateId);
        if (seatingPlan == null)
            return false;

        // Check if the seat is available
        if (!_seatData.Seats[seat.Row -1, seat.Col-1])
            return false;

        // Mark the seat as claimed
        _seatData.Seats[seat.Row-1, seat.Col-1] = false;

        // Save the updated seating plan to the database
        seatingPlan.Seats = _seatData.Seats; // Assuming `Seats` is a serializable array or collection
        _context.SeatingPlan.Update(seatingPlan);

        // Persist the changes
        await _context.SaveChangesAsync();

        return true;
    }

    public void ClaimSeats(List<Seat> seat, int movieDateId)
    {
        foreach (Seat s in seat)
        {
            ClaimSeat(s, movieDateId);
        }
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
    public bool[,] GetAllSeats(int movieShowDateId)
    {
        // return _seatData.Seats;
        return _context.SeatingPlan.FirstOrDefault(s => s.TheaterShowDateID == movieShowDateId)?.Seats;
    }
}
