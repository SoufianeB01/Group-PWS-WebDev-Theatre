using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TheaterShowController : ControllerBase
{
    private readonly ITheaterShowService _showService;
    private readonly  List<Reservation> _context;
    private readonly  List<Customer> _context_customer;
    private IReservationService _reservationService;
    private ISeatService _seatService;   
    public TheaterShowController(ITheaterShowService showService,List<Reservation> context,ISeatService seatService,IReservationService reservationService)
    {
        _reservationService = reservationService;
        _context = context;
        _seatService = seatService;
        _showService = showService;
    }

    [HttpGet]
    public IActionResult GetAllShows()
    {
        var shows = _showService.GetAllShows();
        return Ok(shows);
    }
    
   [HttpGet("{id}")]
    public IActionResult GetShowById(int id)
    {
        var show = _showService.GetShowById(id);
        if (show == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }
        return Ok(show);
    }

    [HttpGet("filter")]
    public IActionResult GetFilteredShows(
        int? id,
        string title = null,
        string description = null,
        int? venueId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string sortBy = "title",
        bool ascending = true)
    {
        var shows = _showService.GetFilteredShows(id, title, description, venueId, startDate, endDate, sortBy, ascending);
        return Ok(shows);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShow([FromBody] TheaterShow newShow)
    {
        await _showService.CreateShow(newShow);
        return CreatedAtAction(nameof(GetShowById), new { id = newShow.TheaterShowID }, newShow);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShows([FromBody] object input)
    {
    if (input == null)
    {
        return BadRequest("Input cannot be null.");
    }
    var createdShows = new List<TheaterShow>();
    if (input is List<TheaterShow> listshows)
    {
        if (!listshows.Any())
        {
            return BadRequest("The list of shows can't be empty.");
        }
        foreach (var show in listshows)
        {
            await _showService.CreateShow(show);
            createdShows.Add(show);
        }
    }
    else if (input is Dictionary<string, TheaterShow> ShowsDict)
    {
        if (!ShowsDict.Any())
        {
            return BadRequest("The dictionary of shows can't be empty.");
        }
        foreach (var pair in ShowsDict)
        {
            var show = pair.Value;
            await _showService.CreateShow(show);
            createdShows.Add(show);
        }
    }
    else
    {
        return BadRequest("Input must be either a list of TheaterShow objects or a dictionary.");
    }

    return CreatedAtAction(nameof(GetShowById), new { ids = createdShows.Select(s => s.TheaterShowID).ToArray() }, createdShows);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShow(int id, [FromBody] TheaterShow updatedShow)
    {
        var existingShow = _showService.GetShowById(id);
        if (existingShow == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }

        updatedShow.TheaterShowID = id;
        await _showService.UpdateShow(updatedShow);
        return NoContent();
    }
    [HttpPut("{id}/mark-as-used")]
    public async Task<IActionResult> MarkAsUsed(Seat seat)
    {
        
        
        if (seat == null) return NotFound();
        else
        {
            var preneeded = new SeatController(_seatService);
            var needed = await preneeded.ClaimSeat(seat);
        

        return NoContent();
        }
    }

    [HttpDelete("{id}")]
    protected async Task<IActionResult> DeleteShow(int id)
    {
        var existingShow = _showService.GetShowById(id);
        if (existingShow == null)
        {
            return NotFound($"Show with ID {id} not found.");
        }

        await _showService.DeleteShow(id);
        return NoContent();
    }


     [HttpGet("All_Reservations")]
    protected async Task<IActionResult> getReservations()
    {
        foreach (var item in _context.Where(x=>x==x).ToList())
        {
            Console.WriteLine($"First name: {_context_customer.First(x=>x.CustomerId==item.CustomerID).FirstName}");
            Console.WriteLine($"Last name: {_context_customer.First(x=>x.CustomerId==item.CustomerID).LastName}");
            Console.WriteLine($"Email: {_context_customer.First(x=>x.CustomerId==item.CustomerID).Email}");
            Console.WriteLine($"Tickets: {item.tickets}");
            Console.WriteLine($"{item.TheatereShowDate}");
           
        }
        return Ok();
    }

    public async Task<IActionResult> GetReservations([FromQuery] int? showId, [FromQuery] DateTime? date, [FromQuery] string search)
    {
        var reservationsQuery = _context.AsQueryable();       
        if (showId.HasValue)
        {
            reservationsQuery = reservationsQuery.Where(x => x.TheatereShowDate.TheaterShowID == showId.Value);
        }
        if (date.HasValue)
        {
            reservationsQuery = reservationsQuery.Where(x => x.TheatereShowDate.Date == date.Value.Date);
        }
        if (!string.IsNullOrEmpty(search))
        {
            
            reservationsQuery = reservationsQuery.Where(x => x.tickets.Any(x =>x.ToString().Contains(search)));
        }
        var reservations = reservationsQuery.ToList();
        return Ok(reservations);
    }

}
