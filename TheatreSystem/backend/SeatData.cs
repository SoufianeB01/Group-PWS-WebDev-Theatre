public record Seat(int Row, int Col);

public class SeatData
{
    public bool[,] Seats { get; set; }

    public SeatData()
    {
        Seats = new bool[10, 10];
        Console.WriteLine("SeatData created");
        // set everyting to true
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Seats[i, j] = true;
            }
        }
    }

}