// TestsApi/Controllers/TestsController.cs
using Microsoft.AspNetCore.Mvc;
using Core.Abstraction;
using Core.Abstraction.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TestsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IRoomService _roomService;
        private readonly IQuestionService _questionService;

        public TestsController(
            ITestService testService,
            IRoomService roomService,
            IQuestionService questionService)
        {
            _testService = testService;
            _roomService = roomService;
            _questionService = questionService;
        }

        // === АДМИНСКИЕ МЕТОДЫ (только для админов) ===

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateTest([FromBody] CreateFullTestDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var test = await _testService.CreateFullTestAsync(dto, userId);
                return CreatedAtAction(nameof(GetTest), new { id = test.Id }, test);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при создании теста", details = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateTest(Guid id, [FromBody] CreateFullTestDto dto)
        {
            try
            {
                var updatedTest = await _testService.UpdateFullTestAsync(id, dto);
                if (updatedTest == null)
                    return NotFound(new { error = "Тест не найден" });

                return Ok(updatedTest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при обновлении теста", details = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            try
            {
                var result = await _testService.DeleteTestAsync(id);
                if (!result)
                    return NotFound(new { error = "Тест не найден" });

                return Ok(new { message = "Тест успешно удален" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при удалении теста", details = ex.Message });
            }
        }

        // === МЕТОДЫ ДЛЯ ПОДТВЕРЖДЕННЫХ ПОЛЬЗОВАТЕЛЕЙ ===

        [HttpPost("my")]
        [Authorize(Policy = "ConfirmedEmailPolicy")]
        public async Task<IActionResult> CreateMyTest([FromBody] CreateFullTestDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var test = await _testService.CreateFullTestAsync(dto, userId);
                return CreatedAtAction(nameof(GetMyTest), new { id = test.Id }, test);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при создании теста", details = ex.Message });
            }
        }

        [HttpGet("my")]
        [Authorize(Policy = "ConfirmedEmailPolicy")]
        public async Task<IActionResult> GetMyTests()
        {
            try
            {
                var userId = GetCurrentUserId();
                var tests = await _testService.GetUserTestsAsync(userId);
                return Ok(tests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении тестов пользователя", details = ex.Message });
            }
        }

        // === ОБЩИЕ МЕТОДЫ ===

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTests()
        {
            try
            {
                var tests = await _testService.GetAllTestsAsync();
                return Ok(tests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении тестов", details = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetTest(Guid id)
        {
            try
            {
                var test = await _testService.GetTestByIdAsync(id);
                if (test == null)
                    return NotFound(new { error = "Тест не найден" });

                return Ok(test);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении теста", details = ex.Message });
            }
        }

        [HttpGet("my/{id:guid}")]
        [Authorize(Policy = "ConfirmedEmailPolicy")]
        public async Task<IActionResult> GetMyTest(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var test = await _testService.GetTestByIdAsync(id);

                if (test == null)
                    return NotFound(new { error = "Тест не найден" });

                if (test.Users_id != userId)
                    return StatusCode(403, new { error = "Доступ запрещен" });

                return Ok(test);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении теста", details = ex.Message });
            }
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
    }
}