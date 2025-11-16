using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Abstraction;
using Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace TestsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        /// <summary>
        /// Получить все тесты
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTests()
        {
            // TODO: Получить список всех тестов из базы данных
            // TODO: Преобразовать в TestDto и вернуть
            return Ok(new List<TestDto>());
        }

        /// <summary>
        /// Получить тест по ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetTest(Guid id)
        {
            // TODO: Найти тест по ID в базе данных
            // TODO: Если не найден - вернуть 404
            // TODO: Преобразовать в TestDto и вернуть
            return Ok(new TestDto(Guid.NewGuid(), "Test Title", Guid.NewGuid()));
        }

        /// <summary>
        /// Создать новый тест
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTest([FromBody] CreateTestDto dto)
        {
            // TODO: Валидация данных
            // TODO: Создать новый тест с помощью Test.CreateTest()
            // TODO: Сохранить в базу данных
            // TODO: Вернуть созданный тест с ID
            return Ok(new TestDto(Guid.NewGuid(), dto.Title, dto.Users_id));
        }

        /// <summary>
        /// Обновить тест
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateTest(Guid id, [FromBody] CreateTestDto dto)
        {
            // TODO: Найти тест по ID
            // TODO: Если не найден - вернуть 404
            // TODO: Обновить данные теста
            // TODO: Сохранить изменения в базу
            return Ok(new TestDto(id, dto.Title, dto.Users_id));
        }

        /// <summary>
        /// Удалить тест
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            // TODO: Найти тест по ID
            // TODO: Если не найден - вернуть 404
            // TODO: Удалить тест из базы данных
            // TODO: Вернуть подтверждение удаления
            return Ok(new { message = "Test deleted successfully" });
        }

        /// <summary>
        /// Получить все комнаты для теста
        /// </summary>
        [HttpGet("{testId:guid}/rooms")]
        [Authorize]
        public async Task<IActionResult> GetTestRooms(Guid testId)
        {
            // TODO: Найти тест по ID
            // TODO: Получить все комнаты связанные с этим тестом
            // TODO: Преобразовать в RoomDto и вернуть
            return Ok(new List<RoomDto>());
        }

        /// <summary>
        /// Создать комнату для теста
        /// </summary>
        [HttpPost("{testId:guid}/rooms")]
        [Authorize]
        public async Task<IActionResult> CreateRoom(Guid testId, [FromBody] CreateRoomDto dto)
        {
            // TODO: Проверить что testId совпадает с dto.Test_id
            // TODO: Создать комнату с помощью Room.CreateRoom()
            // TODO: Сохранить в базу данных
            // TODO: Вернуть созданную комнату
            return Ok(new RoomDto(Guid.NewGuid(), dto.ExitKeyWord, dto.Test_id));
        }

        /// <summary>
        /// Получить все вопросы для комнаты
        /// </summary>
        [HttpGet("rooms/{roomId:guid}/questions")]
        [Authorize]
        public async Task<IActionResult> GetRoomQuestions(Guid roomId)
        {
            // TODO: Найти комнату по ID
            // TODO: Получить все вопросы связанные с этой комнатой
            // TODO: Преобразовать в QuestionDto и вернуть
            return Ok(new List<QuestionDto>());
        }

        /// <summary>
        /// Создать вопрос для комнаты
        /// </summary>
        [HttpPost("rooms/{roomId:guid}/questions")]
        [Authorize]
        public async Task<IActionResult> CreateQuestion(Guid roomId, [FromBody] CreateQuestionDto dto)
        {
            // TODO: Проверить что roomId совпадает с dto.Room_id
            // TODO: Создать вопрос с помощью Question.CreateQuestion()
            // TODO: Сохранить в базу данных
            // TODO: Вернуть созданный вопрос
            return Ok(new QuestionDto(Guid.NewGuid(), dto.Text, dto.ExitKeyLetter, dto.Room_id));
        }

        /// <summary>
        /// Получить полную структуру теста с комнатами и вопросами
        /// </summary>
        [HttpGet("{testId:guid}/full")]
        [Authorize]
        public async Task<IActionResult> GetFullTest(Guid testId)
        {
            // TODO: Найти тест по ID
            // TODO: Загрузить связанные комнаты и вопросы
            // TODO: Сформировать полную структуру и вернуть
            return Ok(new
            {
                Test = new TestDto(Guid.NewGuid(), "Test Title", Guid.NewGuid()),
                Rooms = new List<object>(),
                Questions = new List<object>()
            });
        }

        /// <summary>
        /// Получить тесты текущего пользователя
        /// </summary>
        [HttpGet("my-tests")]
        [Authorize]
        public async Task<IActionResult> GetMyTests()
        {
            // TODO: Получить ID текущего пользователя из токена
            // TODO: Найти все тесты где Users_id = ID текущего пользователя
            // TODO: Вернуть список тестов
            return Ok(new List<TestDto>());
        }
    }
}