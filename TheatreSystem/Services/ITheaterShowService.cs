
namespace TheatreSystem.Services
{
public interface ITheaterShowService
{
    List<TheaterShow> GetAllShows();
    TheaterShow GetShowById(int id);
    Task CreateShow(TheaterShow show);
    Task UpdateShow(TheaterShow show);
    Task DeleteShow(int id);
    List<TheaterShow> GetFilteredShows(
        int? id,
        string title = null,
        string description = null,
        int? venueId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string sortBy = "title",
        bool ascending = true);
}
}