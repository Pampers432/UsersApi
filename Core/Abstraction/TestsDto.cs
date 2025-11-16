namespace Core.Abstraction
{
    public record TestDto(
        Guid Id,
        string Title,
        Guid Users_id
    );

    public record RoomDto(
        Guid Id,
        string ExitKeyWord,
        Guid Test_id
    );

    public record QuestionDto(
        Guid Id,
        string Text,
        string ExitKeyLetter,
        Guid Room_id
    );

    public record CreateTestDto(
        string Title,
        Guid Users_id
    );

    public record CreateRoomDto(
        string ExitKeyWord,
        Guid Test_id
    );

    public record CreateQuestionDto(
        string Text,
        string ExitKeyLetter,
        Guid Room_id
    );
}