using Core.Models;
using Core.Abstraction;
using Core.Abstraction.IServices;
using Core.Abstraction.IRepositories;

namespace Core.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IQuestionRepository _questionRepository;

        public TestService(ITestRepository testRepository, IRoomRepository roomRepository, IQuestionRepository questionRepository)
        {
            _testRepository = testRepository;
            _roomRepository = roomRepository;
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<TestDto>> GetAllTestsAsync()
        {
            var tests = await _testRepository.GetAllAsync();
            return tests.Select(MapToDto);
        }

        public async Task<TestDto?> GetTestByIdAsync(Guid id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            return test == null ? null : MapToDto(test);
        }

        public async Task<FullTestDto?> GetFullTestAsync(Guid id)
        {
            var test = await _testRepository.GetByIdWithRoomsAsync(id);
            if (test == null) return null;

            var roomDtos = new List<RoomWithQuestionsDto>();

            foreach (var room in test.Rooms)
            {
                var questions = await _questionRepository.GetByRoomIdAsync(room.Id);
                var questionDtos = questions.Select(q => new QuestionDto(q.Id, q.Text, q.ExitKeyLetter.ToString(), q.Room_id)).ToList();

                roomDtos.Add(new RoomWithQuestionsDto(room.Id, room.ExitKeyWord, room.Test_id, questionDtos));
            }

            return new FullTestDto(test.Id, test.Title, test.Users_id, roomDtos);
        }

        public async Task<TestDto> CreateTestAsync(CreateTestDto dto, Guid userId)
        {
            var test = Test.CreateTest(dto.Title, userId);
            await _testRepository.AddAsync(test);
            return MapToDto(test);
        }

        public async Task<FullTestDto> CreateFullTestAsync(CreateFullTestDto dto, Guid userId)
        {
            var test = Test.CreateTest(dto.Title, userId);
            await _testRepository.AddAsync(test);

            foreach (var roomDto in dto.Rooms)
            {
                var room = Room.CreateRoom(roomDto.ExitKeyWord, test.Id);
                await _roomRepository.AddAsync(room);

                foreach (var questionDto in roomDto.Questions)
                {
                    var question = Question.CreateQuestion(questionDto.Text, questionDto.ExitKeyLetter[0], room.Id);
                    await _questionRepository.AddAsync(question);
                }
            }

            return await GetFullTestAsync(test.Id);
        }

        public async Task<TestDto?> UpdateTestAsync(Guid id, CreateTestDto dto)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null) return null;

            // TODO: Добавить логику обновления
            await _testRepository.UpdateAsync(test);
            return MapToDto(test);
        }

        public async Task<FullTestDto?> UpdateFullTestAsync(Guid id, CreateFullTestDto dto)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null) return null;

            // TODO: Добавить логику обновления
            await _testRepository.UpdateAsync(test);
            return await GetFullTestAsync(id);
        }

        public async Task<bool> DeleteTestAsync(Guid id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null) return false;

            await _testRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TestDto>> GetUserTestsAsync(Guid userId)
        {
            var tests = await _testRepository.GetByUserIdAsync(userId);
            return tests.Select(MapToDto);
        }

        public async Task<bool> TestExistsAsync(Guid id)
        {
            return await _testRepository.ExistsAsync(id);
        }

        public async Task<FullTestDto?> CloneTestAsync(Guid sourceTestId, Guid newUserId)
        {
            var sourceTest = await GetFullTestAsync(sourceTestId);
            if (sourceTest == null) return null;

            var createDto = new CreateFullTestDto(
                $"{sourceTest.Title} (Копия)",
                sourceTest.Rooms.Select(r => new CreateRoomWithQuestionsDto(
                    r.ExitKeyWord,
                    r.Questions.Select(q => new CreateQuestionDto(q.Text, q.ExitKeyLetter)).ToList()
                )).ToList()
            );

            return await CreateFullTestAsync(createDto, newUserId);
        }

        private static TestDto MapToDto(Test test)
        {
            return new TestDto(test.Id, test.Title, test.Users_id);
        }
    }
}