using Core.Models;

namespace Core.Abstraction.IRepositories
{
    public interface ITestRepository
    {
        Task<Test?> GetByIdAsync(Guid id);
        Task<Test?> GetByIdWithRoomsAsync(Guid id);
        Task<IEnumerable<Test>> GetAllAsync();
        Task<IEnumerable<Test>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Test test);
        Task UpdateAsync(Test test);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}