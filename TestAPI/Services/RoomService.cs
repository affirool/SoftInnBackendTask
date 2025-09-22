using TestAPI.Entities;
using TestAPI.Repositories;

namespace TestAPI.Services
{
    public class RoomService(IRoomRepository roomRepository)
    {
        private readonly IRoomRepository _roomRepository = roomRepository;

        public async Task<IEnumerable<Room>> GetAllRooms ()
        {
            return await _roomRepository.GetAllRooms();
        }
    }
}
