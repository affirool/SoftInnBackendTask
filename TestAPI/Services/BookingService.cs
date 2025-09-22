using TestAPI.DTO;
using TestAPI.Entities;
using TestAPI.Repositories;

namespace TestAPI.Services
{
    public class BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IRoomRepository _roomRepository = roomRepository;

        public async Task<IEnumerable<Booking>> GetAllBookings ()
        {
            return await _bookingRepository.GetAllBookings();
        }

        public async Task<IEnumerable<Booking>> GetBookingById (int id)
        {
            return await _bookingRepository.GetBookingById(id);
        }

        public async Task<Booking> AddNewBooking (AddBookingDTO bookingDTO)
        {
            var isAvailable = await _bookingRepository.GetBookingAvailability(bookingDTO);

            if (isAvailable != null)
            {
                throw new Exception($"Room {isAvailable.RoomId} is already booked from {isAvailable.CheckInDate} to {isAvailable.CheckOutDate}.");
            }

            var book = new Booking
            {
                Id = Random.Shared.Next(0, int.MaxValue),
                GuestName = bookingDTO.GuestName,
                RoomId = bookingDTO.RoomId,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate
            };

            await _bookingRepository.AddNewBooking(book);

            await _roomRepository.SetRoomAvailability(book.RoomId);

            return book;
        }
    }
}
