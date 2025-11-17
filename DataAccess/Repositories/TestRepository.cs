using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Abstraction.IRepositories;
using UsersApi.Data;

namespace DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly UsersApiDbContext _context;

        public TestRepository(UsersApiDbContext context)
        {
            _context = context;
        }

        public async Task<Test?> GetByIdAsync(Guid id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<Test?> GetByIdWithRoomsAsync(Guid id)
        {
            return await _context.Tests
                .Include(t => t.Rooms)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Tests
                .Where(t => t.Users_id == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Test test)
        {
            await _context.Tests.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var test = await GetByIdAsync(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tests.AnyAsync(t => t.Id == id);
        }
    }
}