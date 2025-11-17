using Core.Models;

namespace Core.Abstraction.IRepositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(Guid id);
        Task<Room?> GetByIdWithQuestionsAsync(Guid id);
        Task<IEnumerable<Room>> GetByTestIdAsync(Guid testId);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}