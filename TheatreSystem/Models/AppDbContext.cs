using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets for your tables
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<TheaterShow> TheaterShows { get; set; }
    public DbSet<TheaterShowDate> TheaterShowDates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Seat> Seats { get; set; }


    // Override OnModelCreating for additional configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships, keys, etc. if needed
    }
}
