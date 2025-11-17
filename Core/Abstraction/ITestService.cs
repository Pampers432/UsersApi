using Core.Abstraction;

namespace Core.Abstraction.IServices
{
    public interface ITestService
    {
        Task<IEnumerable<TestDto>> GetAllTestsAsync();
        Task<TestDto?> GetTestByIdAsync(Guid id);
        Task<FullTestDto?> GetFullTestAsync(Guid id);
        Task<TestDto> CreateTestAsync(CreateTestDto dto, Guid userId);
        Task<FullTestDto> CreateFullTestAsync(CreateFullTestDto dto, Guid userId);
        Task<TestDto?> UpdateTestAsync(Guid id, CreateTestDto dto);
        Task<FullTestDto?> UpdateFullTestAsync(Guid id, CreateFullTestDto dto);
        Task<bool> DeleteTestAsync(Guid id);
        Task<IEnumerable<TestDto>> GetUserTestsAsync(Guid userId);
        Task<bool> TestExistsAsync(Guid id);
        Task<FullTestDto?> CloneTestAsync(Guid sourceTestId, Guid newUserId);
    }
}