public class TheaterShowPoll
{
    public int Id { get; set; } // poll id
    public string Title { get; set; } // Title of the poll niet van show!!
    public List<TheaterShowOption> TheaterShowOptions { get; set; } = new List<TheaterShowOption>();
}

public class TheaterShowOption
{
    public TheaterShow TheaterShow { get; set; }
    public int VoteCount { get; set; }
}