public class SeatingPlan
{
    public int SeatingPlanID { get; set; }
    public int TheaterShowDateID { get; set; }
    public bool[,] Seats { get; set; }

    public SeatingPlan(int seatingPlanId,int theaterShowDateID)
    {
        SeatingPlanID = seatingPlanId;
        TheaterShowDateID = theaterShowDateID;
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