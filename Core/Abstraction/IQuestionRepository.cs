using Core.Models;

namespace Core.Abstraction.IRepositories
{
    public interface IQuestionRepository
    {
        Task<Question?> GetByIdAsync(Guid id);
        Task<IEnumerable<Question>> GetByRoomIdAsync(Guid roomId);
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}