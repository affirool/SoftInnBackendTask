using TestAPI.DTO;
using TestAPI.Data;
using TestAPI.Entities;
using TestAPI.Repositories;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Tests.Repositories
{
    public class BookingRepositoryTest
    {
        private readonly DataContext _context;
        private readonly BookingRepository _repository;

        public BookingRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _context.Bookings.Add(new Booking {
                Id = Random.Shared.Next(0, int.MaxValue),
                GuestName = "Test Guest Name",
                RoomId = 000,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            });
            _context.SaveChanges();

            _repository = new BookingRepository(_context);
        }

        [Fact]
        public async Task BookingRepositoryTest_GetAllBookings_ReturnBookingList()
        {
            var result = await _repository.GetAllBookings();

            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Booking>>();
        }

        [Fact]
        public async Task BookingRepositoryTest_GetAllBookingById_ReturnBookingList()
        {
            int roomId = 000;
            var result = await _repository.GetBookingById(roomId);

            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Booking>>();
        }

        [Fact]
        public async Task BookingRepositoryTest_GetBookingAvailability_ReturnBookingObject()
        {
            var bookingDTO = new AddBookingDTO
            {
                GuestName = "Test Guest Name",
                RoomId = 000,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };
            var result = await _repository.GetBookingAvailability(bookingDTO);

            result.Should().NotBeNull();
            result.Should().BeOfType<Booking>();
        }

        [Fact]
        public async Task BookingRepositoryTest_GetBookingAvailability_ReturnBookingObjectNull()
        {
            var bookingDTO = new AddBookingDTO
            {
                GuestName = "Test Guest Name",
                RoomId = 001,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };
            var result = await _repository.GetBookingAvailability(bookingDTO);

            result.Should().BeNull();
        }

        [Fact]
        public async Task BookingRepositoryTest_AddNewBooking_ReturnBookingObject()
        {
            var booking = new Booking
            {
                Id = Random.Shared.Next(0, int.MaxValue),
                GuestName = "Test Guest Name",
                RoomId = 001,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };
            var result = await _repository.AddNewBooking(booking);

            result.Should().NotBeNull();
            result.Should().BeOfType<Booking>();
        }
    }
}
