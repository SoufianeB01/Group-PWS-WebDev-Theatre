public class ShowData
{
    public List<TheaterShow> Shows { get; set; }
    public List<TheaterShowDate> ShowDates { get; set; }

    public ShowData()
    {
        Shows = new List<TheaterShow>
        {
            new TheaterShow { TheaterShowID = 1, Title = "Inception", Description = "A mind-bending thriller", Price = 10.0f, VenueID = 1 },
            new TheaterShow { TheaterShowID = 2, Title = "The Dark Knight", Description = "A heroic adventure", Price = 12.0f, VenueID = 2 },
            new TheaterShow { TheaterShowID = 3, Title = "Interstellar", Description = "A journey through space and time", Price = 15.0f, VenueID = 1 },
            new TheaterShow { TheaterShowID = 4, Title = "Parasite", Description = "A thrilling story of class struggle", Price = 14.0f, VenueID = 3 },
            new TheaterShow { TheaterShowID = 5, Title = "The Matrix", Description = "A hacker discovers the true nature of reality", Price = 11.0f, VenueID = 2 },
            new TheaterShow { TheaterShowID = 6, Title = "Shutter Island", Description = "A psychological thriller set in a mental institution", Price = 13.0f, VenueID = 3 }
        };

        ShowDates = new List<TheaterShowDate>
        {
            new TheaterShowDate { TheaterShowDateID = 1, TheaterShowID = 1, Date = "2024-10-20", Time = "19:00" },
            new TheaterShowDate { TheaterShowDateID = 2, TheaterShowID = 2, Date = "2024-10-21", Time = "20:00" },
            new TheaterShowDate { TheaterShowDateID = 3, TheaterShowID = 3, Date = "2024-10-22", Time = "18:30" },
            new TheaterShowDate { TheaterShowDateID = 4, TheaterShowID = 4, Date = "2024-10-23", Time = "19:30" },
            new TheaterShowDate { TheaterShowDateID = 5, TheaterShowID = 5, Date = "2024-10-24", Time = "20:00" },
            new TheaterShowDate { TheaterShowDateID = 6, TheaterShowID = 6, Date = "2024-10-25", Time = "21:00" }
        };

        Console.WriteLine("ShowData created with sample shows");
    }
}
