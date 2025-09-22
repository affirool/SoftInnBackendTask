using TestAPI.DTO;
using TestAPI.Services;
using TestAPI.Entities;
using TestAPI.Controllers;
using TestAPI.Repositories;

using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Tests.Controllers
{
    public class BookingControllerTest
    {
        private readonly Fake<IBookingRepository> _bookingRepository;
        private readonly Fake<IRoomRepository> _roomRepository;
        private readonly BookingService _service;
        private readonly BookingController _controller;

        public BookingControllerTest()
        {
            _bookingRepository = new Fake<IBookingRepository>();
            _roomRepository = new Fake<IRoomRepository>();
            _service = new BookingService(_bookingRepository.FakedObject, _roomRepository.FakedObject);
            _controller = new BookingController(_service);
        }

        [Fact]
        public async Task BookingController_GetAllBookings_ReturnBookingList()
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

            var actionResult = await _controller.GetAllBookings();
            var okResult = actionResult.Result as OkObjectResult;
            var bookingsValue = okResult!.Value as IEnumerable<Booking>;

            actionResult.Result.Should().BeOfType<OkObjectResult>();
            okResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BookingController_GetBookingById_ReturnBookingList()
        {
            int id = 000;
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = Random.Shared.Next(0, int.MaxValue),
                    GuestName = "Test Guest Name",
                    RoomId = id,
                    CheckInDate = DateTime.Parse("2025-06-20"),
                    CheckOutDate = DateTime.Parse("2025-06-23")
                }
            };

            A.CallTo(() => _bookingRepository.FakedObject.GetBookingById(id)).Returns(Task.FromResult<IEnumerable<Booking>>(bookings));

            var actionResult = await _controller.GetAllBookings();
            var okResult = actionResult.Result as OkObjectResult;
            var value = okResult!.Value as IEnumerable<Booking>;

            actionResult.Result.Should().BeOfType<OkObjectResult>();
            okResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BookingController_AddNewBooking_ReturnBookingObject()
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

            var actionResult = await _controller.AddNewBooking(bookingDTO);
            var jsonResult = actionResult.Result as JsonResult;
            var bookingsValue = jsonResult!.Value as Booking;

            actionResult.Result.Should().BeOfType<JsonResult>();
            jsonResult.Should().BeOfType<JsonResult>();
        }
    }
}