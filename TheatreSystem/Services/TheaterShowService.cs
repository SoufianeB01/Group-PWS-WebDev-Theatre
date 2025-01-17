public class TheaterShowService : ITheaterShowService
{
    private readonly AppDbContext _context;
    private int _nextId;

    public TheaterShowService(AppDbContext context)
    {
        _context = context;
        _nextId = _context.TheaterShows.Count() + 1;
    }

    public List<TheaterShow> GetAllShows()
    {
        return _context.TheaterShows.ToList();
    }

    public TheaterShow GetShowById(int id)
    {
        return GetAllShows().FirstOrDefault(show => show.TheaterShowID == id);
    }

    public async Task CreateShow(TheaterShow show)
    {
        show.TheaterShowID = _nextId++;
        _context.TheaterShows.Add(show);
        _context.SaveChanges();
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

            _context.SaveChanges();
        }
        await Task.CompletedTask;
    }

    public async Task DeleteShow(int id)
    {
        var show = GetShowById(id);
        if (show != null)
        {
            _context.TheaterShows.Remove(show);
            _context.SaveChanges();
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
        var filteredShows = _context.TheaterShows.ToList();

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
            var showIdsWithMatchingDates = _context.TheaterShowDates
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