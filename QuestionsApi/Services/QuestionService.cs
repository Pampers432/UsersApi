using Core.Models;
using Core.Abstraction;
using Core.Abstraction.IServices;
using Core.Abstraction.IRepositories;

namespace Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<QuestionDto>> GetRoomQuestionsAsync(Guid roomId)
        {
            var questions = await _questionRepository.GetByRoomIdAsync(roomId);
            return questions.Select(MapToDto);
        }

        public async Task<QuestionDto?> GetQuestionByIdAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            return question == null ? null : MapToDto(question);
        }

        public async Task<QuestionDto> CreateQuestionAsync(Guid roomId, CreateQuestionDto dto)
        {
            var question = Question.CreateQuestion(dto.Text, dto.ExitKeyLetter[0], roomId);
            await _questionRepository.AddAsync(question);
            return MapToDto(question);
        }

        public async Task<QuestionDto?> UpdateQuestionAsync(Guid id, CreateQuestionDto dto)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null) return null;

            // TODO: Добавить логику обновления
            await _questionRepository.UpdateAsync(question);
            return MapToDto(question);
        }

        public async Task<bool> DeleteQuestionAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null) return false;

            await _questionRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> QuestionExistsAsync(Guid id)
        {
            return await _questionRepository.ExistsAsync(id);
        }

        private static QuestionDto MapToDto(Question question)
        {
            return new QuestionDto(question.Id, question.Text, question.ExitKeyLetter.ToString(), question.Room_id);
        }
    }
}