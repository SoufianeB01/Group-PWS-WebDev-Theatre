public interface ITheaterShowService
{
    List<TheaterShow> GetAllShows();
    TheaterShow GetShowById(int id);
    Task CreateShow(TheaterShow show);
    Task UpdateShow(TheaterShow show);
    Task DeleteShow(int id);
}
