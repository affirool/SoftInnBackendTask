using TestAPI.Data;
using TestAPI.Entities;
using TestAPI.Repositories;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Tests.Repositories
{
    public class RoomRepositoryTest
    {
        private readonly DataContext _context;
        private readonly RoomRepository _repository;

        public RoomRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _context.Rooms.Add(new Room
            {
                Id = Random.Shared.Next(0, int.MaxValue),
                Name = "Orchid 103",
                Type = "Suite",
                IsAvailable = true
            });
            _context.SaveChanges();

            _repository = new RoomRepository(_context);
        }

        [Fact]
        public async Task RoomRepository_GetAllRooms_ReturnRoomList()
        {
            var result = await _repository.GetAllRooms();

            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Room>>();
        }

        [Fact]
        public async Task RoomRepository_GetAllRooms_ReturnRoomListEmpty()
        {
            //force empty state
            _context.Rooms.RemoveRange(_context.Rooms);
            _context.SaveChanges();

            var result = await _repository.GetAllRooms();

            result.Should().BeEmpty();
        }
    }
}
