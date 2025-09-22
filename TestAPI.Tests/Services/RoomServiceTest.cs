using TestAPI.Services;
using TestAPI.Entities;
using TestAPI.Repositories;

using FakeItEasy;
using FluentAssertions;


namespace TestAPI.Tests.Services
{
    public class RoomServiceTest
    {
        private readonly Fake<IRoomRepository> _roomRepository;
        private readonly RoomService _service;

        public RoomServiceTest()
        {
            _roomRepository = new Fake<IRoomRepository>();
            _service = new RoomService(_roomRepository.FakedObject);
        }

        [Fact]
        public async Task RoomServiceTest_GetAllRooms_ReturnRoomList()
        {
            var rooms = new List<Room>
            {
                new Room
                {
                    Id = Random.Shared.Next(0, int.MaxValue),
                    Name = "Orchid 103",
                    Type = "Suite",
                    IsAvailable = true
                }
            };

            A.CallTo(() => _roomRepository.FakedObject.GetAllRooms()).Returns(Task.FromResult<IEnumerable<Room>>(rooms));

            var result = await _service.GetAllRooms();

            result.Should().NotBeNullOrEmpty();
            result.Should().AllBeOfType<Room>();
            result.Should().BeOfType<List<Room>>();
        }

        [Fact]
        public async Task RoomServiceTest_GetAllRooms_ReturnRoomListEmpty()
        {
            A.CallTo(() => _roomRepository.FakedObject.GetAllRooms()).Returns(Task.FromResult<IEnumerable<Room>>(new List<Room>()));

            var result = await _service.GetAllRooms();

            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Room>>();
        }
    }
}
