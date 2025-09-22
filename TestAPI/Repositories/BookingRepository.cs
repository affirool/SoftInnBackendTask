using TestAPI.DTO;
using TestAPI.Data;
using TestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<IEnumerable<Booking>> GetBookingById(int RoomId);
        Task<Booking?> GetBookingAvailability(AddBookingDTO bookingDTO);
        Task<Booking> AddNewBooking(Booking booking);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _dataContext;

        public BookingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _dataContext.Bookings.ToListAsync() ?? new List<Booking>();
        }

        public async Task<IEnumerable<Booking>> GetBookingById(int roomId)
        {
            return await _dataContext.Bookings.Where(el => el.RoomId == roomId).ToListAsync();
        }

        public async Task<Booking?> GetBookingAvailability(AddBookingDTO bookingDTO)
        {
            var isAvailable = await _dataContext.Bookings.FirstOrDefaultAsync(el =>
                el.RoomId == bookingDTO.RoomId &&
                el.CheckInDate < bookingDTO.CheckOutDate &&
                el.CheckOutDate > bookingDTO.CheckInDate
            );

            return isAvailable;
        }

        public async Task<Booking> AddNewBooking(Booking booking)
        {
            await _dataContext.Bookings.AddAsync(booking);
            await _dataContext.SaveChangesAsync();

            return booking;
        }
    }
}
