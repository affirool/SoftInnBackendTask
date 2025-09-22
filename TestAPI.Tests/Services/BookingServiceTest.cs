using TestAPI.DTO;
using TestAPI.Services;
using TestAPI.Entities;
using TestAPI.Repositories;

using FakeItEasy;
using FluentAssertions;

namespace TestAPI.Tests.Services
{
    public class BookingServiceTest
    {
        private readonly Fake<IBookingRepository> _bookingRepository;
        private readonly Fake<IRoomRepository> _roomRepository;
        private readonly BookingService _service;

        public BookingServiceTest()
        {
           _bookingRepository = new Fake<IBookingRepository>();
           _roomRepository = new Fake<IRoomRepository>();
           _service = new BookingService(_bookingRepository.FakedObject, _roomRepository.FakedObject);
        }

        [Fact]
        public async Task BookingServiceTest_GetAllBookings_ReturnBookingList()
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = Random.Shared.Next(0, int.MaxValue),
                    GuestName = "Test Guest Name",
                    RoomId = 000,
                    CheckInDate = DateTime.Parse("2025-06-20"),
                    CheckOutDate = DateTime.Parse("2025-06-23")
                }
            };

            A.CallTo(() => _bookingRepository.FakedObject.GetAllBookings()).Returns(Task.FromResult<IEnumerable<Booking>>(bookings));

            var result = await _service.GetAllBookings();

            result.Should().NotBeNullOrEmpty();
            result.Should().AllBeOfType<Booking>();
            result.Should().BeOfType<List<Booking>>();
        }

        [Fact]
        public async Task BookingServiceTest_GetAllBookings_ReturnBookingListEmpty()
        {
            var result = await _service.GetAllBookings();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task BookingServiceTest_GetAllBookingById_ReturnBookingList()
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = Random.Shared.Next(0, int.MaxValue),
                    GuestName = "Test Guest Name",
                    RoomId = 000,
                    CheckInDate = DateTime.Parse("2025-06-20"),
                    CheckOutDate = DateTime.Parse("2025-06-23")
                }
            };

            A.CallTo(() => _bookingRepository.FakedObject.GetAllBookings()).Returns(Task.FromResult<IEnumerable<Booking>>(bookings));

            var result = await _service.GetAllBookings();

            result.Should().NotBeNullOrEmpty();
            result.Should().AllBeOfType<Booking>();
            result.Should().BeOfType<List<Booking>>();
        }

        [Fact]
        public async Task BookingServiceTest_GetAllBookingById_ReturnBookingListEmpty()
        {
            var result = await _service.GetAllBookings();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task BookingServiceTest_AddNewBooking_ReturnBookingObject()
        {
            var booking = new Booking
            {
                Id = Random.Shared.Next(0, int.MaxValue),
                GuestName = "Test Guest Name",
                RoomId = 000,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };

            var bookingDTO = new AddBookingDTO
            {
                GuestName = "Test Guest Name",
                RoomId = 000,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };

            A.CallTo(() => _bookingRepository.FakedObject.GetBookingAvailability(bookingDTO)).Returns(Task.FromResult<Booking?>(null));
            A.CallTo(() => _bookingRepository.FakedObject.AddNewBooking(A<Booking>.Ignored)).ReturnsLazily((Booking b) => Task.FromResult(b));
            A.CallTo(() => _roomRepository.FakedObject.SetRoomAvailability(booking.Id)).Returns(Task.FromResult<Boolean>(false));

            var result = await _service.AddNewBooking(bookingDTO);

            result.Should().NotBeNull();
            result.Should().BeOfType<Booking>();
        }

        [Fact]
        public async Task BookingServiceTest_AddNewBooking_ReturnConflict()
        {
            var bookingDTO = new AddBookingDTO
            {
                GuestName = "Test Guest Name",
                RoomId = 000,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };

            var booking = new Booking
            {
                Id = Random.Shared.Next(0, int.MaxValue),
                GuestName = "Test Guest Name",
                RoomId = 000,
                CheckInDate = DateTime.Parse("2025-06-20"),
                CheckOutDate = DateTime.Parse("2025-06-23")
            };

            // force GetBookingAvailability = false
            A.CallTo(() => _bookingRepository.FakedObject.GetBookingAvailability(bookingDTO)).Returns(Task.FromResult<Booking?>(booking));

            await FluentActions.Invoking(() => _service
                .AddNewBooking(bookingDTO))
                .Should().ThrowAsync<Exception>();
        }
    }
}
