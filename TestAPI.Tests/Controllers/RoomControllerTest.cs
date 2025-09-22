using TestAPI.Entities;
using TestAPI.Services;
using TestAPI.Controllers;
using TestAPI.Repositories;

using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Tests.Controllers
{
    public class RoomControllerTest
    {
        private readonly Fake<IRoomRepository> _repository;
        private readonly RoomService _service;
        private readonly RoomController _controller;

        public RoomControllerTest()
        {
            _repository = new Fake<IRoomRepository>();
            _service = new RoomService(_repository.FakedObject);
            _controller = new RoomController(_service);
        }

        public async Task RoomController_GetAllRooms_ReturnRoomList()
        {
            var rooms = new List<Room>{
                new Room
                {
                    Id = 1983746592,
                    Name = "Orchid 105",
                    Type = "Double",
                    IsAvailable = true
                }
            };

            A.CallTo(() => _repository.FakedObject.GetAllRooms()).Returns(Task.FromResult<IEnumerable<Room>>(rooms));

            var actionResult = await _controller.GetAllRooms();
            var okResult = actionResult.Result as OkObjectResult;
            var value = okResult!.Value as IEnumerable<Room>;

            actionResult.Result.Should().BeOfType<OkObjectResult>();
            okResult.Should().BeOfType<OkObjectResult>();
            value.Should().AllBeOfType<Room>();
            value.Should().BeOfType<List<Room>>();
        }
    }
}
