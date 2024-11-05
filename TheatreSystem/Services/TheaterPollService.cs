public class TheaterShowPollService
{
    private List<TheaterShowPoll> _theaterShowPolls;

   
    public TheaterShowPollService()
    {
        _theaterShowPolls = new List<TheaterShowPoll>
        {
            new TheaterShowPoll
            {
                Id = 1,
                Title = "Weekend Movie Poll",
                TheaterShowOptions = new List<TheaterShowOption>
                {
                    new TheaterShowOption { TheaterShow = new TheaterShow { TheaterShowID = 1, Title = "Inception", Description = "A mind-bending thriller", Price = 10.0f, VenueID = 1 }, VoteCount = 0 },
                    new TheaterShowOption { TheaterShow = new TheaterShow { TheaterShowID = 2, Title = "The Dark Knight", Description = "A heroic adventure", Price = 12.0f, VenueID = 2 }, VoteCount = 0 },
                    new TheaterShowOption { TheaterShow = new TheaterShow { TheaterShowID = 3, Title = "Interstellar", Description = "A journey through space and time", Price = 15.0f, VenueID = 1 }, VoteCount = 0 },
                    new TheaterShowOption { TheaterShow = new TheaterShow { TheaterShowID = 4, Title = "Parasite", Description = "A thrilling story of class struggle", Price = 14.0f, VenueID = 3 }, VoteCount = 0 },
                    new TheaterShowOption { TheaterShow = new TheaterShow { TheaterShowID = 5, Title = "The Matrix", Description = "A hacker discovers the true nature of reality", Price = 11.0f, VenueID = 2 }, VoteCount = 0 }
                }
            }
        };

        Console.WriteLine("TheaterShowPollService created with sample polls.");
    }

    public IEnumerable<TheaterShowPoll> GetAllPolls()
    {
        return _theaterShowPolls;
    }

    public TheaterShowPoll GetPollById(int id)
    {
        return _theaterShowPolls.FirstOrDefault(p => p.Id == id);
    }

    public void VoteForShow(int pollId, int theaterShowIndex)
    {
        var poll = _theaterShowPolls.FirstOrDefault(p => p.Id == pollId);
        if (poll != null && theaterShowIndex >= 0 && theaterShowIndex < poll.TheaterShowOptions.Count)
        {
            poll.TheaterShowOptions[theaterShowIndex].VoteCount++;
        }
    }

    public void AddTheaterShowPoll(TheaterShowPoll poll)
    {
        if (poll.TheaterShowOptions.Count != 5)
        {
            throw new System.Exception("A poll must contain exactly 5 theater shows.");
        }

        poll.Id = _theaterShowPolls.Count + 1; // Simple ID assignment
        _theaterShowPolls.Add(poll);
    }
}