using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Abstraction.IRepositories;
using UsersApi.Data;

namespace DataAccess.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly UsersApiDbContext _context;

        public RoomRepository(UsersApiDbContext context)
        {
            _context = context;
        }

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room?> GetByIdWithQuestionsAsync(Guid id)
        {
            return await _context.Rooms
                .Include(r => r.Questions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Room>> GetByTestIdAsync(Guid testId)
        {
            return await _context.Rooms
                .Where(r => r.Test_id == testId)
                .ToListAsync();
        }

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var room = await GetByIdAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == id);
        }
    }
}