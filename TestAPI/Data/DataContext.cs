using TestAPI.Helpers;
using TestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Data
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options) {}

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public async Task seed()
        {
            if (!Rooms.Any())
            {
                var rooms = await SeedHelper.LoadSeedData<Room>("Data/static/rooms.json");
                await Rooms.AddRangeAsync(rooms);
            }

            if (!Bookings.Any())
            {
                var booking = await SeedHelper.LoadSeedData<Booking>("Data/static/bookings.json");
                await Bookings.AddRangeAsync(booking);
            }

            await SaveChangesAsync();
        }
    }
}
