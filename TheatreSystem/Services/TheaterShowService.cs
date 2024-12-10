#pragma warning disable CA1050 // Declare types in namespaces
public class TheaterShowService : ITheaterShowService
#pragma warning restore CA1050 // Declare types in namespaces
{
    private readonly List<TheaterShow> _shows;
    private readonly List<TheaterShowDate> _showDates;
    private int _nextId;

    public TheaterShowService()
    {
        var showData = new ShowData();
        _shows = showData.Shows;
        _showDates = showData.ShowDates;
        _nextId = _shows.Count + 1;
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

    public List<TheaterShow> GetFilteredShows
    (
        int? id,
        string title = null,
        string description = null,
        int? venueId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string sortBy = "title",
        bool ascending = true)
    {
        var filteredShows = _shows.ToList();

        if (id.HasValue)
        {
            var showById = filteredShows.FirstOrDefault(show => show.TheaterShowID == id.Value);
            return showById != null ? new List<TheaterShow> { showById } : new List<TheaterShow>();
        }

        if (!string.IsNullOrEmpty(title))
        {
            filteredShows = filteredShows
                .Where(show => show.Title.Contains(title))
                .ToList();
        }

        if (!string.IsNullOrEmpty(description))
        {
            filteredShows = filteredShows
                .Where(show => show.Description.Contains(description))
                .ToList();
        }

        if (venueId.HasValue)
        {
            filteredShows = filteredShows
                .Where(show => show.VenueID == venueId.Value)
                .ToList();
        }

        if (startDate.HasValue || endDate.HasValue)
        {
            var showIdsWithMatchingDates = _showDates
                .Where(date => 
                    (!startDate.HasValue || DateTime.Parse(date.Date) >= startDate.Value) && 
                    (!endDate.HasValue || DateTime.Parse(date.Date) <= endDate.Value))
                .Select(date => date.TheaterShowID)
                .Distinct()
                .ToList();

            filteredShows = filteredShows
                .Where(show => showIdsWithMatchingDates.Contains(show.TheaterShowID))
                .ToList();
        }

        filteredShows = sortBy.ToLower() switch
        {
            "title" => ascending ? filteredShows.OrderBy(show => show.Title).ToList() : filteredShows.OrderByDescending(show => show.Title).ToList(),
            "price" => ascending ? filteredShows.OrderBy(show => show.Price).ToList() : filteredShows.OrderByDescending(show => show.Price).ToList(),
            _ => ascending ? filteredShows.OrderBy(show => show.Title).ToList() : filteredShows.OrderByDescending(show => show.Title).ToList()
        };

        return filteredShows;
    }
}