using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Abstraction.IRepositories;
using UsersApi.Data;

namespace DataAccess.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly UsersApiDbContext _context;

        public QuestionRepository(UsersApiDbContext context)
        {
            _context = context;
        }

        public async Task<Question?> GetByIdAsync(Guid id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<IEnumerable<Question>> GetByRoomIdAsync(Guid roomId)
        {
            return await _context.Questions
                .Where(q => q.Room_id == roomId)
                .ToListAsync();
        }

        public async Task AddAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var question = await GetByIdAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Questions.AnyAsync(q => q.Id == id);
        }
    }
}