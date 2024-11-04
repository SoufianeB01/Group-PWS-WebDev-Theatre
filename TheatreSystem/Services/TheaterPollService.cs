using System.Collections.Generic;
using System.Linq;

public class TheaterShowPollService
{
    public IEnumerable<TheaterShowPoll> GetAllPolls()
    {
        return PollContext.TheaterShowPolls;
    }

    public TheaterShowPoll GetPollById(int id)
    {
        return PollContext.TheaterShowPolls.FirstOrDefault(p => p.Id == id);
    }

    public void VoteForShow(int pollId, int theaterShowIndex)
    {
        var poll = PollContext.TheaterShowPolls.FirstOrDefault(p => p.Id == pollId);
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

        poll.Id = PollContext.TheaterShowPolls.Count + 1; // Simple ID assignment
        PollContext.TheaterShowPolls.Add(poll);
    }
}
