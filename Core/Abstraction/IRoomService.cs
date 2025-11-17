using Core.Abstraction;

namespace Core.Abstraction.IServices
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetTestRoomsAsync(Guid testId);
        Task<RoomDto?> GetRoomByIdAsync(Guid id);
        Task<RoomDto> CreateRoomAsync(Guid testId, CreateRoomDto dto);
        Task<RoomDto?> UpdateRoomAsync(Guid id, CreateRoomDto dto);
        Task<bool> DeleteRoomAsync(Guid id);
        Task<RoomDto?> GetRoomWithQuestionsAsync(Guid id);
        Task<bool> RoomExistsAsync(Guid id);
    }
}