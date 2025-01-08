public class TheaterShowDateService : ITheaterShowDateService
{
    private readonly AppDbContext _context;
    private int _nextId;

    public TheaterShowDateService(AppDbContext context)
    {
        _context = context;
        _nextId = _context.TheaterShowDates.Count() + 1;
    }

    public List<TheaterShowDate> GetAllShowDates()
    {
        return _context.TheaterShowDates.ToList();
    }

    public TheaterShowDate GetShowDateById(int id)
    {
        return GetAllShowDates().FirstOrDefault(showDate => showDate.TheaterShowDateID == id);
    }

    public async Task CreateShowDate(TheaterShowDate showDate)
    {
        showDate.TheaterShowDateID = _nextId++;
        _context.TheaterShowDates.Add(showDate);
        _context.SaveChanges();
        await Task.CompletedTask;
    }

    public async Task UpdateShowDate(TheaterShowDate updatedShowDate)
    {
        var existingShowDate = GetShowDateById(updatedShowDate.TheaterShowDateID);
        if (existingShowDate != null)
        {
            existingShowDate.Date = updatedShowDate.Date;
            existingShowDate.Time = updatedShowDate.Time;
            existingShowDate.TheaterShowID = updatedShowDate.TheaterShowID;

            _context.SaveChanges();
        }
        await Task.CompletedTask;
    }

    public async Task DeleteShowDate(int id)
    {
        var showDate = GetShowDateById(id);
        if (showDate != null)
        {
            _context.TheaterShowDates.Remove(showDate);
            _context.SaveChanges();
        }
        await Task.CompletedTask;
    }

    public List<TheaterShowDate> GetFilteredShowDates(
        int? id,
        string date = null,
        string time = null,
        int? theaterShowId = null,
        string sortBy = "date",
        bool ascending = true)
    {
        var showDates = _context.TheaterShowDates.ToList();

        if (id.HasValue)
        {
            showDates = showDates.Where(showDate => showDate.TheaterShowDateID == id).ToList();
        }

        if (date != null)
        {
            showDates = showDates.Where(showDate => showDate.Date == date).ToList();
        }

        if (time != null)
        {
            showDates = showDates.Where(showDate => showDate.Time == time).ToList();
        }

        if (theaterShowId.HasValue)
        {
            showDates = showDates.Where(showDate => showDate.TheaterShowID == theaterShowId).ToList();
        }

        if (sortBy == "date")
        {
            showDates = ascending ? showDates.OrderBy(showDate => showDate.Date).ToList() : showDates.OrderByDescending(showDate => showDate.Date).ToList();
        }
        else if (sortBy == "time")
        {
            showDates = ascending ? showDates.OrderBy(showDate => showDate.Time).ToList() : showDates.OrderByDescending(showDate => showDate.Time).ToList();
        }
        else if (sortBy == "theaterShowId")
        {
            showDates = ascending ? showDates.OrderBy(showDate => showDate.TheaterShowID).ToList() : showDates.OrderByDescending(showDate => showDate.TheaterShowID).ToList();
        }

        return showDates;
    }
}