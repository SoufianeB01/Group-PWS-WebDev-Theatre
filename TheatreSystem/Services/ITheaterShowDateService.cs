public interface ITheaterShowDateService
{
    List<TheaterShowDate> GetAllShowDates();
    TheaterShowDate GetShowDateById(int id);
    Task CreateShowDate(TheaterShowDate showDate);
    Task UpdateShowDate(TheaterShowDate showDate);
    Task DeleteShowDate(int id);
    List<TheaterShowDate> GetFilteredShowDates(
        int? id,
        string date = null,
        string time = null,
        int? theaterShowId = null,
        string sortBy = "date",
        bool ascending = true);
}
