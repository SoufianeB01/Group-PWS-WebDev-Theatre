public class TheaterShowService : ITheaterShowService
{
    private readonly List<TheaterShow> _shows;
    private int _nextId;

    public TheaterShowService()
    {
        var showData = new ShowData(); // Initialize with sample data
        _shows = showData.Shows;
        _nextId = _shows.Count + 1; // Set nextId to the next available ID
    }

    public List<TheaterShow> GetAllShows()
    {
        return _shows;
    }

    public TheaterShow GetShowById(int id)
    {
        return _shows.FirstOrDefault(show => show.TheaterShowID == id);
    }

    public async Task CreateShow(TheaterShow show)
    {
        show.TheaterShowID = _nextId++;
        _shows.Add(show);
        await Task.CompletedTask;
    }

    public async Task UpdateShow(TheaterShow updatedShow)
    {
        var existingShow = GetShowById(updatedShow.TheaterShowID);
        if (existingShow != null)
        {
            existingShow.Title = updatedShow.Title;
            existingShow.Description = updatedShow.Description;
            existingShow.Price = updatedShow.Price;
            existingShow.VenueID = updatedShow.VenueID;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteShow(int id)
    {
        var show = GetShowById(id);
        if (show != null)
        {
            _shows.Remove(show);
        }
        await Task.CompletedTask;
    }
}
