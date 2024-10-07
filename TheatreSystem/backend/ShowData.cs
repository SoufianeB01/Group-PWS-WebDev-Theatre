public class ShowData
{
    public List<TheaterShow> Shows { get; set; }

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

        Console.WriteLine("ShowData created with sample shows");
    }
}
