using Core.Abstraction;

namespace Core.Abstraction.IServices
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetRoomQuestionsAsync(Guid roomId);
        Task<QuestionDto?> GetQuestionByIdAsync(Guid id);
        Task<QuestionDto> CreateQuestionAsync(Guid roomId, CreateQuestionDto dto);
        Task<QuestionDto?> UpdateQuestionAsync(Guid id, CreateQuestionDto dto);
        Task<bool> DeleteQuestionAsync(Guid id);
        Task<bool> QuestionExistsAsync(Guid id);
    }
}