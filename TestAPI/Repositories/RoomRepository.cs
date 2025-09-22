using TestAPI.Data;
using TestAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Entities.Room>> GetAllRooms();
        Task<bool> SetRoomAvailability(int Id);
    }

    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _dataContext;

        public RoomRepository (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _dataContext.Rooms.ToListAsync() ?? new List<Room>();
        }

        public async Task<bool> SetRoomAvailability(int Id)
        {
            var room = await _dataContext.Rooms.FirstOrDefaultAsync(el => el.Id == Id);

            if (room == null) {
                return false;
            }

            room.IsAvailable = !room.IsAvailable;

            await _dataContext.SaveChangesAsync();

            return true;
        }
    }
}
