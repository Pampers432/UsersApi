namespace Core.Abstraction
{
    public record TestDto(
        int Id,
        string Title,
        int Users_id
    );

    public record RoomDto(
        int Id,
        string ExitKeyWord,
        int Test_id
    );

    public record QuestionDto(
        int Id,
        string Text,
        string ExitKeyLetter,
        int Room_id
    );

    public record CreateTestDto(
        string Title,
        int Users_id
    );

    public record CreateRoomDto(
        string ExitKeyWord,
        int Test_id
    );

    public record CreateQuestionDto(
        string Text,
        string ExitKeyLetter,
        int Room_id
    );
}