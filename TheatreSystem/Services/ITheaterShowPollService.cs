public interface ITheaterShowPollService
{
    IEnumerable<TheaterShowPoll> GetAllPolls();
    TheaterShowPoll GetPollById(int id);
    void VoteForShow(int pollId, int theaterShowIndex);
    void AddTheaterShowPoll(TheaterShowPoll poll);
}