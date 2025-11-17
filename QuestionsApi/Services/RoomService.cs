using Core.Models;
using Core.Abstraction;
using Core.Abstraction.IServices;
using Core.Abstraction.IRepositories;

namespace Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IQuestionRepository _questionRepository;

        public RoomService(IRoomRepository roomRepository, IQuestionRepository questionRepository)
        {
            _roomRepository = roomRepository;
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<RoomDto>> GetTestRoomsAsync(Guid testId)
        {
            var rooms = await _roomRepository.GetByTestIdAsync(testId);
            return rooms.Select(MapToDto);
        }

        public async Task<RoomDto?> GetRoomByIdAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            return room == null ? null : MapToDto(room);
        }

        public async Task<RoomDto> CreateRoomAsync(Guid testId, CreateRoomDto dto)
        {
            var room = Room.CreateRoom(dto.ExitKeyWord, testId);
            await _roomRepository.AddAsync(room);
            return MapToDto(room);
        }

        public async Task<RoomDto?> UpdateRoomAsync(Guid id, CreateRoomDto dto)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return null;

            // TODO: Добавить логику обновления
            await _roomRepository.UpdateAsync(room);
            return MapToDto(room);
        }

        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            await _roomRepository.DeleteAsync(id);
            return true;
        }

        public async Task<RoomDto?> GetRoomWithQuestionsAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdWithQuestionsAsync(id);
            if (room == null) return null;

            var roomDto = MapToDto(room);
            // TODO: Добавить вопросы в DTO
            return roomDto;
        }

        public async Task<bool> RoomExistsAsync(Guid id)
        {
            return await _roomRepository.ExistsAsync(id);
        }

        private static RoomDto MapToDto(Room room)
        {
            return new RoomDto(room.Id, room.ExitKeyWord, room.Test_id);
        }
    }
}